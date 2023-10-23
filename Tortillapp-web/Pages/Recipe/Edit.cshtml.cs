using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class EditModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public EditModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecipeInfo Recipe { get; set; } = default!;
        [BindProperty]
        public IList<RecipeStep> Step { get; set; } = default!;
        [BindProperty]
        public IList<RecipeIngredient> Ingredient { get; set; } = default!;

        List<string> units = new List<string>()
        {
            "",
            "Pieza",
            "Litro",
            "Mililitro",
            "Cuarto de kilo",
            "Medio kilo",
            "Kilo",
            "Gramo",
            "Cucharada",
            "Cucharadita",
            "Media taza",
            "Taza",
            "Pizca",
            "Al gusto"

        };
        public SelectList Itype { get; set; }

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            Itype = new SelectList(units);
 
            if (id == null || _context.RecipeInfos == null)
            {
                return NotFound();
            }

            var recipeinfo =  await _context.RecipeInfos.FirstOrDefaultAsync(m => m.RecipeId == id);

            if (recipeinfo == null)
            {
                return NotFound();
            }

            Recipe = recipeinfo;
            Ingredient = await _context.RecipeIngredients
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();
            Step = await _context.RecipeSteps
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeInfoExists(Recipe.RecipeId))
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

        private bool RecipeInfoExists(ushort id)
        {
          return (_context.RecipeInfos?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }

    }
}
