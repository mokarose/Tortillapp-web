using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class CreateModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public CreateModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.UserDatas, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public RecipeInfo RecipeInfo { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
