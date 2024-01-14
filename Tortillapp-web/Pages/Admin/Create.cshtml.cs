using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public CreateModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public SelectList Role { get; set; }

        public IActionResult OnGet()
        {
            //ViewData["RoleId"] = new SelectList(_context.UserRoles, "RoleId", "RoleName");
            Role = new SelectList(_context.UserRoles, nameof(UserRole.RoleId), nameof(UserRole.RoleName));
             
            return Page();
        }

        [BindProperty]
        public UserData UserData { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.UserDatas == null || UserData == null)
              {
                  return Page();
              }

              _context.UserDatas.Add(UserData);
              await _context.SaveChangesAsync();

              return RedirectToPage("./Index");

            /*var emptyUser = UserData;

            if (await TryUpdateModelAsync<UserData>(
                emptyUser,
                "",   // Prefix for form value.
                u => u.UserMail,
                u => u.UserName, 
                u => u.UserPass, 
                u => u.RoleId,
                u => u.ShowName,
                u => u.ShowPic))
            {
                _context.UserDatas.Add(emptyUser);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();*/
        }
    }
}
