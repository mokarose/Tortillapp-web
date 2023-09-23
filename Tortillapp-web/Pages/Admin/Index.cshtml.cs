using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public IndexModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<UserData> UserData { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserDatas != null)
            {
                UserData = await _context.UserDatas
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}
