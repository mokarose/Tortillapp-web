using System;
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
    public class ViewUsersModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public ViewUsersModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<UserData> UserData { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserData != null)
            {
                UserData = await _context.UserData
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}

