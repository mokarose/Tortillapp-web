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

namespace Tortillapp_web.Pages.Recipe
{
    public class CreateModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public CreateModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public partial class _Ingredient
        {
            public string? IngredientName { get; set; }
            public short? IngredientAmount { get; set; }
            public string? IngredientUnit { get; set; }
        }

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
        [TempData]
        public string rname { get; set; }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["UserId"] = new SelectList(_context.UserDatas, "UserId", "UserId"); //Usuario loggeado será el dueño
            Itype = new SelectList(units);

            string iUser = HttpContext.Session.GetString("Usuario"); //Usuario actual

            if (iUser == null) //|| _context.UserDatas == null)
            {
                return NotFound();
            }

            var user = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserName == iUser);

            if (user == null)
            {
                return NotFound();
            }

            if (user.ShowName != null)
            {
                TempData["rname"] = user.ShowName;
            }
            else
            {
                TempData["rname"] = user.UserName;
            }

            UserD = user;
            ruser = UserD.UserId;

            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "¡Falta nombre de la receta!")]
        [Display(Name = "Titulo")]
        public string rtitle { get; set; }
        [BindProperty]
        public short rportion { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "¡Falta el tiempo de preparación!")]
        public TimeSpan rtime { get; set; }
        //[BindProperty]
        //public byte rpic { get; set; }
        [BindProperty]
        public string rprep { get; set; }
        [BindProperty]
        public string rtips { get; set; }
        [BindProperty]
        public ushort ruser { get; set; }
        [BindProperty]
        public _Ingredient[] Ingredients { get; set; } = default!;
        //Models
        public RecipeIngredient[] Ingredient { get; set; } = default!;
        public RecipeStep[] Steps { get; set; } = default!;
        public RecipeInfo Recipe { get; set; } = default!;
        public IList<RecipeTag> Tags { get; set; } = default!;
        public UserData UserD { get; set; } = default!;
        public string merror { get; set; }

        [HttpPost]
        public IActionResult OnPostBackToIndex(string btnCancel)
        {
            if (btnCancel.Equals("Cancelar"))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        /***[HttpPost]
        public IActionResult GetTitleRecipe(string btnContinue)
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
                    //TempData[nameof(rtitle)] = rtitle;
                    //TempData["rname"] = UserD.ShowName;
                    //ruser = iUser.UserId;
                }
            }
            return Page();
        }***/

        [HttpPost]
        public IActionResult OnPostSaveDraft(string btnDraft)
        {
            if (btnDraft.Equals("Guardar borrador"))
            {
                if (rprep == null){ rprep = "";}
                if (rtips == null) { rtips = "";}
                _context.RecipeInfos.Add(new RecipeInfo
                {
                    UserId = ruser,
                    RecipeTitle = rtitle,
                    RecipeTime = rtime,
                    RecipePortion = rportion,
                    RecipeTips = rtips,
                    Published = DateTime.Now

                });
                _context.SaveChanges();

                ushort last_insert = _context.RecipeInfos.Max(r => r.RecipeId);

                AddASteps(rprep, last_insert);
                AddIngredients(last_insert);
            }
            return RedirectToPage("Index", "");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) //|| _context.RecipeInfos == null || RecipeInfo == null)
            {
                return Page();
            }

            //var userOwner = await _context.UserDatas.FindAsync(ruser);

            /***if (userOwner == null)
            {
                return NotFound();
            }***/

            _context.RecipeInfos.Add(new RecipeInfo
            {
                UserId = ruser,
                RecipeTitle = rtitle,
                RecipeTime = rtime,
                RecipePortion = rportion,
                RecipeTips = rtips,
                RecipeStatus = 2,
                Published = DateTime.Now

            });

            await _context.SaveChangesAsync();

            ushort last_insert = _context.RecipeInfos.Max(r => r.RecipeId);

            AddASteps(rprep, last_insert);
            AddIngredients(last_insert);

            return RedirectToPage("Index", "");
        }

        void AddASteps(string Step, ushort ID)
        {
            string[] stepsi;
            byte pos = 1;

            if (Step != null)
            {
                stepsi = Step.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                foreach (string step in stepsi)
                {
                    _context.RecipeSteps.Add(new RecipeStep
                    {
                        RecipeId = ID,
                        StepPos = pos,
                        StepDescrp = step
                    });
                    pos++;
                    _context.SaveChanges();
                }
            }
        }

        void AddIngredients(ushort ID)
        {
            if (Ingredients != null)
            {
                foreach (_Ingredient ingredients in Ingredients)
                {
                    _context.RecipeIngredients.Add(new RecipeIngredient
                    {
                        RecipeId = ID,
                        IngredientName = ingredients.IngredientName,
                        IngredientAmount = ingredients.IngredientAmount,
                        IngredientUnit = ingredients.IngredientUnit
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
