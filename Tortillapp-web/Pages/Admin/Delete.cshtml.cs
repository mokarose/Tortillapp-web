﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public DeleteModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserData UserData { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            if (id == null || _context.UserData == null)
            {
                return NotFound();
            }

            var userdata = await _context.UserData.FirstOrDefaultAsync(m => m.UserId == id);

            if (userdata == null)
            {
                return NotFound();
            }
            else 
            {
                UserData = userdata;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ushort? id)
        {
            if (id == null || _context.UserData == null)
            {
                return NotFound();
            }
            var userdata = await _context.UserData.FindAsync(id);

            if (userdata != null)
            {
                UserData = userdata;
                _context.UserData.Remove(UserData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
