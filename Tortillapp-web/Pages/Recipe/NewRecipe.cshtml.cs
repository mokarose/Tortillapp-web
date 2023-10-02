using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Metadata;
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

        List<string> units = new List<string>()
        {
            "",
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

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.UserDatas, "UserId", "UserId"); //Usuario loggeado será el dueño
            
            Itype = new SelectList(units);

            return Page();
        }

        [BindProperty]
        public string rtitle { get; set; }
        [BindProperty]
        public short rportion { get; set; }
        [BindProperty]
        public TimeSpan rtime { get; set; }
        //[BindProperty]
        //public byte rpic { get; set; }
        [BindProperty]
        public string rprep { get; set; }
        [BindProperty]
        public string rtips { get; set; }
        public RecipeIngredient Ingredient { get; set; } = default!;
        public RecipeStep Steps { get; set; } = default!;
        public RecipeInfo Recipe { get; set; } = default!;
        public IList<RecipeTag> Tags { get; set; } = default!;
        public UserData UserData { get; set; } = default!;
        public string merror { get; set; }

        [HttpPost]
        public IActionResult OnPostBackToIndex(string btnCancel)
        {
            if(btnCancel.Equals("Cancelar"))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        //[HttpPost]
        public IActionResult OnPostCreateRecipe(string btnContinue)
        {
            if (btnContinue.Equals("Siguiente"))
            {
                if (rtitle == null) 
                {
                    merror = "¡Falta nombre de la receta!";
                }
                else
                {
                    //Ocultar esta vista y mostrar la siguiente
                    //Mostrar nombre elegido en todo el siguiente paso
                    //TitleSection.Visible = false;
                }
            }
            return Page();
        }

        //[HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) //|| _context.RecipeInfos == null || RecipeInfo == null)
            {
                return Page();
            }

            _context.RecipeInfos.Add(new RecipeInfo 
            {
                UserId = UserData.UserId,
                RecipeTitle = rtitle,
                RecipeTime = rtime,
                RecipePortion = rportion,
                RecipeTips = rtips,
                Published = DateTime.Now

            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
