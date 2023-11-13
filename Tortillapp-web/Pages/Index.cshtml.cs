using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //private CConnection nConnect;
        //public string itag => (string)TempData[nameof(itag)];

        /*public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
		}*/

        private readonly Tortillapp_web.Data.tortillaContext _context;

        public IndexModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public IList<Tag> Tags { get; set; } = default!;

        [BindProperty]
        public string toSearch { get; set; }
        public List<float> rscore { get; set; } = new List<float>();

        public async Task OnGetAsync()
        {
            int iter = 0;
            if (_context.RecipeInfos != null)
            {
                RecipeInfo = await _context.RecipeInfos
                .Include(r => r.User).ToListAsync();

                Tags = await _context.Tags.ToListAsync();
            }
            foreach (var recipe in RecipeInfo)
            {
                rscore.Add(GetRecipeRating(recipe.RecipeId));
                iter++;
            }
        }

        public IActionResult OnGetLogout() 
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToPage("Index");
        }
        
        public IActionResult OnPostSearch()
        {
            if (toSearch == null)
            {
                return RedirectToPage("Index");
            }
            return Redirect("/Search?search=" + toSearch );
        }

        public float GetRecipeRating(ushort id_recipe)
        {
            float sumScore = 0;
            float scoreTotal = 0;

            var scoreall = _context.Scores
                .Where(r => r.Title == id_recipe.ToString()).ToList();

            if (scoreall != null)
            {
                for (int i = 0; i < scoreall.Count(); i++)
                {
                    sumScore += scoreall[i].ScorePoints;
                }
                scoreTotal = sumScore / scoreall.Count();
            }
            else
            {
                scoreTotal = 0;
            }

            return scoreTotal;
        }

    }
}