using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;
using Microsoft.AspNetCore.Authorization;

namespace Tortillapp_web.Pages.Receta
{
    public class IndexModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public IndexModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            string iUser = HttpContext.Session.GetString("Usuario");

            if (iUser == null)
            {
                return NotFound();
            }
            
            if (_context.RecipeInfos != null)
            {
                RecipeInfo = await _context.RecipeInfos
                //.Include(r => r.User).ToListAsync();
                .Where(r => r.User.UserName == iUser).ToListAsync();
            }

            return Page();
        }
    }
}
