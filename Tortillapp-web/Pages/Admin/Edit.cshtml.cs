using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public EditModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserData UserData { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            if (id == null || _context.UserDatas == null)
            {
                return NotFound();
            }

            var userdata =  await _context.UserDatas.FirstOrDefaultAsync(m => m.UserId == id);
            if (userdata == null)
            {
                return NotFound();
            }
            UserData = userdata;
           ViewData["RoleId"] = new SelectList(_context.UserRoles, "RoleId", "RoleId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(UserData.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserDataExists(ushort id)
        {
          return (_context.UserDatas?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
