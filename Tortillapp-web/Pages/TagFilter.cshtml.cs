using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Text;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages
{
    public class TagFilterModel : PageModel
    {

        private readonly Tortillapp_web.Data.tortillaContext _context;

        public TagFilterModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public List<string> rpicto { get; set; } = new List<string>();
        public List<float> rscore { get; set; } = new List<float>();
        public List<string> tags { get; set; } = new List<string>();
        List<List<string>> listOfLists = new List<List<string>>();

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            List<RecipeInfo> iRecipe = new List<RecipeInfo>();

            var query = (from t in _context.Tags
                        join rt in _context.RecipeTags
                        on t.TagId equals rt.TagId
                        where rt.TagId == id
                        select rt.RecipeId).ToList();


            foreach ( var recipe in query )
            {
                RecipeInfo = _context.RecipeInfos
                    .Where(r => r.RecipeId.Equals(recipe)).ToList();

                rscore.Add(GetRecipeRating(RecipeInfo[0].RecipeId));

                if (RecipeInfo[0].RecipePic != null)
                {
                    rpicto.Add(Load(RecipeInfo[0].RecipePic));
                }
                else
                {
                    rpicto.Add("tortilla-basic-cuadro.jpg");
                }
                var recipeTags = _context.RecipeTags.
                        Where(t => t.RecipeId == RecipeInfo[0].RecipeId).ToList();

                if (recipeTags != null)
                {
                    foreach (var tag in recipeTags)
                    {
                        var thistag = _context.Tags.FirstOrDefault(t => t.TagId == tag.TagId);
                        tags.Add(thistag.TagName);
                    }
                }
                listOfLists.Add(tags);
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
