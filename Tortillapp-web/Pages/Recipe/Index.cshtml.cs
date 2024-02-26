using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace Tortillapp_web.Pages.Receta
{
    public class IndexModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public IndexModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public List<string> rpicto { get; set; } = new List<string>();
        public List<float> rscore { get; set; } = new List<float>();

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            string iUser = HttpContext.Session.GetString("Usuario");

            if (iUser == null)
            {
                return NotFound();
            }
            
            if (_context.RecipeInfos != null)
            {
                RecipeInfo = await _context.RecipeInfos
                //.Include(r => r.User).ToListAsync();
                .Where(r => r.User.UserName == iUser).ToListAsync();
            }

            foreach (var recipeInfo in RecipeInfo)
            {
                rscore.Add(GetRecipeRating(recipeInfo.RecipeId));
                if (recipeInfo.RecipePic != null)
                {
                    rpicto.Add(Load(recipeInfo.RecipePic));
                }
                else
                {
                    rpicto.Add("tortilla-basic-cuadro.jpg");
                }
            }

            return Page();
        }
        public float GetRecipeRating(ushort id_recipe)
        {
            float sumScore = 0;
            float scoreTotal = 0;

            try
            {
                var scoreall = _context.UserRatings
                    .Where(r => r.RecipeId == id_recipe)
                    .Average(r => r.ScorePoints).ToString();

                scoreTotal = float.Parse(scoreall);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return scoreTotal;
        }

        public string Load(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
