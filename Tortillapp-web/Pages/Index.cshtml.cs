using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //private CConnection nConnect;
        //public string itag => (string)TempData[nameof(itag)];

        /*public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
		}*/

        private readonly Tortillapp_web.Data.tortillaContext _context;

        public IndexModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public IList<RecipeTag> Tags { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RecipeInfos != null)
            {
                RecipeInfo = await _context.RecipeInfos
                .Include(r => r.User).ToListAsync();
            }
        }

        public IActionResult OnGetLogout() 
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToPage("Index");
        }
        
    }
}