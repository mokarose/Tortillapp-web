using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages
{
    public class FirstViewModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public FirstViewModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? search)
        {
            string[] strSplitSearch = null;
            if (search != null)
            {
                RecipeInfo = await _context.RecipeInfos
                    .Where(r => r.RecipeTitle.Contains(search)).ToListAsync();

                if (RecipeInfo.Count <= 0)
                {
                    strSplitSearch = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    /*RecipeInfo = await _context.RecipeInfos
                        .Join(_context.RecipeIngredients,
                        r => r,
                        i => i.Recipe,
                        (r, i) =>
                            new
                            {
                                rName = r.RecipeTitle,
                                iName = i.IngredientName
                            });*/
                        //.Where(e => e.iName.Contains(strSplitSearch[0]))
                        //.ToListAsync();
                }
            }
            return Page();
        }

        /*public async Task<IActionResult> OnGetSearch(string? search)
        {
            
        }*/
    }
}
