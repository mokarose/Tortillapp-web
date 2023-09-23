using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class NewRecipeModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public NewRecipeModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.UserDatas, "UserId", "UserId"); //Usuario loggeado será el dueño
            return Page();
        }

        [BindProperty]
        public RecipeInfo RecipeInfo { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.RecipeInfos == null || RecipeInfo == null)
            {
                return Page();
            }

            _context.RecipeInfos.Add(RecipeInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
