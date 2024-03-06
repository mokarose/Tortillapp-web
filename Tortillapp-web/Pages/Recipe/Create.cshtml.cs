using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages.Recipe
{
    public class CreateModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;
        private IWebHostEnvironment _environment;

        public CreateModel(Tortillapp_web.Data.tortillaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public partial class _Ingredient //Modelo para los ingredientes
        {
            public string? IngredientName { get; set; }
            public short IngredientAmount { get; set; }
            public string? IngredientUnit { get; set; }
        }

        List<string> units = new List<string>() //Lista de medidas
        {
            "",
            "Pieza",
            "Media pieza",
            "Cuarto de pieza",
            "Litro",
            "Medio litro",
            "Mililitro",
            "Cuarto de kilo",
            "Medio kilo",
            "Kilo",
            "Kilo y medio",
            "Gramo",
            "Cucharada",
            "Cucharadita",
            "Media taza",
            "Taza",
            "Taza y media",
            "Tres cuartos de taza",
            "Pizca",
            "Al gusto"

        };

        public SelectList Itype { get; set; }
        [TempData]
        public string rname { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "¡Falta nombre de la receta!")]
        [Display(Name = "Titulo")]
        public string rtitle { get; set; }
        [BindProperty]
        public short rportion { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "¡Falta el tiempo de preparación!")]
        public TimeSpan rtime { get; set; }
        [BindProperty]
        public string rprep { get; set; }
        [BindProperty]
        public string rtips { get; set; }
        [BindProperty]
        public ushort ruser { get; set; }
        [BindProperty]
        public _Ingredient[] Ingredient { get; set; } //= default!;
        [BindProperty]
        public string[] tagit { get; set; }
        public IFormFile rimage { get; set; }
        public string picto { get; set; }
        public string userpic { get; set; }
        //Models
        public RecipeIngredient[] Ingredients { get; set; } = default!;
        public RecipeStep[] Steps { get; set; } = default!;
        public RecipeInfo Recipe { get; set; } = default!;
        public IList<RecipeTag> Tags { get; set; } = default!;
        public UserData UserD { get; set; } = default!;
        public string rmerror { get; set; }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            //ViewData["UserId"] = new SelectList(_context.UserDatas, "UserId", "UserId"); //Usuario loggeado será el dueño
            //Itype = new SelectList(units);
            Itype = new SelectList(units);

            string iUser = HttpContext.Session.GetString("Usuario"); //Usuario actual

            if (iUser == null) //|| _context.UserDatas == null)
            {
                return NotFound();
            }

            var user = await _context.UserData.FirstOrDefaultAsync(u => u.UserName == iUser);

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
            picto = "imagen-azul-receta.jpg";
            if (user.ShowPic != null)
            {
                userpic = Load(user.ShowPic);
            }
            else
            {
                userpic = "profile1.png";
            }
            return Page();
        }

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
            byte[]? bytes = null;
            if (btnDraft.Equals("Guardar borrador"))
            {
                if (rprep == null){ rprep = "";}
                if (rtips == null) { rtips = "";}
                if (rimage != null)
                {
                    bytes = Upload(rimage);
                }
                _context.RecipeInfos.Add(new RecipeInfo
                {
                    UserId = ruser,
                    RecipeTitle = rtitle,
                    RecipeTime = rtime,
                    RecipePortion = rportion,
                    RecipeTips = rtips,
                    RecipePic = bytes,
                    Published = DateTime.Now

                });
                _context.SaveChanges();

                ushort last_insert = _context.RecipeInfos.Max(r => r.RecipeId);

                AddASteps(rprep, last_insert);
                AddIngredients(last_insert);
                AddTags(last_insert);
            }
            return RedirectToPage("Index", "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            byte[]? bytes = null;

            /*if (!ModelState.IsValid) //|| _context.RecipeInfos == null || RecipeInfo == null)
            {
                return Page();
            }*/

            if (rtitle != null)
            {
                if (rportion==0 || rtime.TotalMinutes == 0)
                {
                    TempData["rmerror"] = "Completa los datos si quieres Publicar, o Guarda el borrador";
                    return this.Page();
                }
                
                if (rimage != null)
                {
                    bytes = Upload(rimage);
                }

                _context.RecipeInfos.Add(new RecipeInfo
                {
                    UserId = ruser,
                    RecipeTitle = rtitle,
                    RecipeTime = rtime,
                    RecipePortion = rportion,
                    RecipeTips = rtips,
                    RecipeStatus = 2,
                    RecipePic = bytes,
                    Published = DateTime.Now

                });

                await _context.SaveChangesAsync();

                ushort last_insert = _context.RecipeInfos.Max(r => r.RecipeId);

                AddASteps(rprep, last_insert);
                AddIngredients(last_insert);
                AddTags(last_insert);

                return RedirectToPage("Index", "");
            }

            return this.Page();
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
            if (Ingredient != null)
            {
                foreach (_Ingredient ingredients in Ingredient)
                {
                    if (ingredients.IngredientName != null)
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

        void AddTags(ushort ID)
        {
            if (tagit != null)
            {
                foreach(string tag in tagit)
                {
                    if (tag != null)
                    {
                        var atags = _context.Tags.FirstOrDefault(r => r.TagName == tag);
                        if (atags != null)
                        {
                            _context.RecipeTags.Add(new RecipeTag
                            {
                                RecipeId = ID,
                                TagId = atags.TagId,
                                TagAdded = DateTime.Now
                            });
                        }
                        else
                        {
                            _context.Tags.Add(new Tag
                            {
                                TagName = tag,
                                TagCreated = DateTime.Now
                            });

                            _context.SaveChanges();
                            ushort last_insert = _context.Tags.Max(t => t.TagId);

                            _context.RecipeTags.Add(new RecipeTag
                            {
                                RecipeId = ID,
                                TagId = last_insert,
                                TagAdded = DateTime.Now
                            });
                        }
                        _context.SaveChanges();
                    }
                }
            }
        }

        public byte[] Upload(IFormFile image)
        {
            string wwwPath = this._environment.WebRootPath;
            byte[] data = null;
            string filepath = null;

            string path = Path.Combine(wwwPath, "pics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filename = Path.GetFileName(image.FileName);

            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                if (stream.Length <= 5242880)
                {
                    image.CopyTo(stream);
                }
                else
                {
                    TempData["merror"] = "El archivo es muy grande (5MB max)";
                }
            }

            bool exists = System.IO.File.Exists(Path.Combine(path, filename));
            if (exists)
            {
                filepath = System.Guid.NewGuid().ToString();
                System.IO.File.Move(Path.Combine(path, filename), Path.Combine(path, Path.ChangeExtension(filepath, ".jpg")));
                System.IO.File.Delete(Path.Combine(path, filename));
            }
            else
            {
                TempData["merror"] = "Hay un problema para copiar la imagen";
            }

            using (FileStream file = new FileStream(Path.Combine(path, Path.ChangeExtension(filepath, ".jpg")), FileMode.Create))
            {
                image.CopyTo(file);

                using (MemoryStream memory = new MemoryStream())
                {
                    file.Write(memory.ToArray());
                    data = Encoding.ASCII.GetBytes(Path.ChangeExtension(filepath, ".jpg"));
                }
            }
            return data;
        }

        public string Load(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
