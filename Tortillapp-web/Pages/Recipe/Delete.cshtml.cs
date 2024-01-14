using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class DeleteModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public DeleteModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RecipeInfo RecipeInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            if (id == null || _context.RecipeInfos == null)
            {
                return NotFound();
            }

            var recipeinfo = await _context.RecipeInfos.FirstOrDefaultAsync(m => m.RecipeId == id);

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

        public async Task<IActionResult> OnPostAsync(ushort? id)
        {
            if (id == null || _context.RecipeInfos == null)
            {
                return NotFound();
            }
            var recipeinfo = await _context.RecipeInfos.FindAsync(id);

            if (recipeinfo != null)
            {
                RecipeInfo = recipeinfo;
                _context.RecipeInfos.Remove(RecipeInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
