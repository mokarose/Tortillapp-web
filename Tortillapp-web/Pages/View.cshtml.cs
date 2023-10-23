using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public RecipeModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }
        public RecipeInfo RecipeInfo { get; set; } = default!;
        public IList<RecipeStep> Step { get; set; } = default!;
        [BindProperty]
        public IList<RecipeIngredient> Ingredient { get; set; } = default!;
        public IList<RecipeTag> Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            if (id == null || _context.RecipeInfos == null)
            {
                return NotFound();
            }

            var recipeinfo = await _context.RecipeInfos.FirstOrDefaultAsync(m => m.RecipeId == id);
            Ingredient = await _context.RecipeIngredients
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();
            Step = await _context.RecipeSteps
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();
            Tag = await _context.RecipeTags
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();

            if (recipeinfo == null)
            {
                return NotFound();
            }
            else
            {
                RecipeInfo = recipeinfo;
            }
            return Page();
        }
    }
}
