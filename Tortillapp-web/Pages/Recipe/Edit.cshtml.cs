using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Model;
using System.Text;

namespace Tortillapp_web.Pages.Receta
{
    public class EditModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;
        private IWebHostEnvironment _environment;

        public EditModel(Tortillapp_web.Data.tortillaContext context, IWebHostEnvironment environment)
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

        [BindProperty]
        public RecipeInfo Recipe { get; set; } = default!;
        public IList<RecipeStep> RecipeStep { get; set; } = default!;
        [BindProperty]
        public IList<RecipeIngredient> Ingredient { get; set; } = default!;
        public IList<Tag> Tags { get; set; } = default!;
        public IList<RecipeTag> RecipeTags { get; set; } = default!;
        [BindProperty]
        public string RecipePrep { get; set; }
        [BindProperty]
        public string[] TagIt { get; set; }
        //public List<string> TagIt = new List<string>();
        public string picto { get; set; }
        public IFormFile rimage { get; set; }


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
            var steps = await _context.RecipeSteps
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();

            RecipePrep = GetSteps(steps);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            byte[]? bytes = null;

            var recipeToUpdate = await _context.RecipeInfos.FindAsync(Recipe.RecipeId);

            if (recipeToUpdate == null)
            {
                return BadRequest();
            }

            if (rimage != null)
            {
                bytes = Upload(rimage);
                if (bytes != null)
                {
                    if (recipeToUpdate.RecipePic != null)
                    {
                        Delete(Load(recipeToUpdate.RecipePic));
                    }
                    Recipe.RecipePic = bytes;
                }
            }
            else
            {
                Recipe.RecipePic = recipeToUpdate.RecipePic;
            }

            Recipe.RecipeTitle = recipeToUpdate.RecipeTitle;
            Recipe.RecipeTime = recipeToUpdate.RecipeTime;
            Recipe.RecipePortion = recipeToUpdate.RecipePortion;
            Recipe.RecipeTips = recipeToUpdate.RecipeTips;
            Recipe.Published = DateTime.Now;

            AddTags(recipeToUpdate.RecipeId);

            _context.Entry(recipeToUpdate).CurrentValues.SetValues(Recipe);
            _context.Entry(recipeToUpdate).State = EntityState.Modified;
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            //_context.Entry()
            //_context.Attach(Recipe).State = EntityState.Modified;

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

            return RedirectToPage("Index", "");
        }

        private bool RecipeInfoExists(ushort id)
        {
          return (_context.RecipeInfos?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }

        private string GetSteps(IList<RecipeStep> recipeStep)
        {
            string allsteps = "";
            foreach(var step in recipeStep)
            {
                allsteps += step.StepDescrp + "\n";
            }
            return allsteps;
        }
        void AddTags(ushort ID)
        {
            if (TagIt != null)
            {
                foreach (string tag in TagIt)
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

        void UpdateSteps(string Step, ushort ID)
        {
            string[] stepsi;
            byte pos = 1;

            var steps = _context.RecipeSteps
                .Where(r => r.RecipeId == ID).ToList();

            RecipeStep = steps;

            if (Step != null)
            {
                stepsi = Step.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                foreach (string step in stepsi)
                {
                    if (RecipeStep[pos].StepPos == pos)
                    {
                        RecipeStep[pos].StepDescrp = step;
                    }
                    else
                    {
                        _context.RecipeSteps.Add(new RecipeStep
                        {
                            RecipeId = ID,
                            StepPos = pos,
                            StepDescrp = step
                        });
                    }
                    pos++;
                    _context.SaveChanges();
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

        public void Delete(string filename)
        {
            string wwwPath = this._environment.WebRootPath;
            string path = Path.Combine(wwwPath, "pics");
            System.IO.File.Delete(Path.Combine(path, filename));
        }

    }
}
