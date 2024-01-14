using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class DetailsModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public DetailsModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public RecipeInfo RecipeInfo { get; set; } = default!;
        public UserFavorite UserFavorites { get; set; } = default!;
        public IList<RecipeStep> Step { get; set; } = default!;
        [BindProperty]
        public IList<RecipeIngredient> Ingredient { get; set; } = default!;
        public IList<RecipeTag> Tag { get; set; } = default!;
        public List<Tag> NameTags { get; set; } = new List<Tag>();
        public List<Score> AllScore { get; set; } = default!;
        public UserRating Score { get; set; } = default!;
        public UserData User { get; set; } = default!;
        public UserData UserLogged { get; set; } = default!;
        public string picRecipe { get; set; }
        public string picUser { get; set; }
        public string userShow { get; set; }
        public string picUserLog { get; set; }
        [BindProperty]
        public ushort actualUserLog { get; set; }
        public string userShowLog { get; set; }
        [BindProperty]
        public ushort idRecipe { get; set; }
        [BindProperty]
        public ushort uscore { get; set; }
        [BindProperty]
        public string ucomment { get; set; }
        public float rscore { get; set; }

        public async Task<IActionResult> OnGetAsync(ushort? id)
        {
            if (id == null || _context.RecipeInfos == null)
            {
                return NotFound();
            }

            var recipeinfo = await _context.RecipeInfos.FirstOrDefaultAsync(m => m.RecipeId == id);
            Ingredient = await _context.RecipeIngredients
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();
            Step = await _context.RecipeSteps
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();
            var tags = await _context.RecipeTags
                .Where(r => r.RecipeId == recipeinfo.RecipeId).ToListAsync();

            var scorerating = await _context.UserRatings.FirstOrDefaultAsync(s => s.RecipeId == id);

            var userinfo = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserId == recipeinfo.UserId);

            if (recipeinfo == null)
            {
                return NotFound();
            }
            else
            {
                RecipeInfo = recipeinfo;
                User = userinfo;
                Score = scorerating;
                idRecipe = RecipeInfo.RecipeId;
                Tag = tags;

                if (Score != null)
                {
                    rscore = GetRecipeRating(idRecipe);
                }
                else
                {
                    rscore = 0;
                }

                if (RecipeInfo.RecipePic != null)
                {
                    picRecipe = Load(RecipeInfo.RecipePic);
                }
                else
                {
                    picRecipe = "tortilla-basic-rectangulo.jpg";
                }

                if (User.ShowPic != null)
                {
                    picUser = Load(User.ShowPic);
                }
                else
                {
                    picUser = "profile2.png";
                }

                if (User.ShowName != null)
                {
                    userShow = User.ShowName;
                }
                else
                {
                    userShow = User.UserName;
                }

                if (Tag.Count > 0)
                {
                    foreach (var tag in Tag)
                    {
                        var nametags = await _context.Tags.FirstOrDefaultAsync(t => t.TagId == tag.TagId);
                        NameTags.Add(nametags);
                    }

                }

                var userfavorite = await _context.UserFavorites.Where(r => r.RecipeId == RecipeInfo.RecipeId).ToListAsync();
                foreach (var fav in userfavorite)
                {
                    if (fav.UserId == actualUserLog)
                    {
                        UserFavorites = fav;
                    }
                }
            }
            return Page();
        }

        public float GetRecipeRating(ushort id_recipe)
        {
            float sumScore = 0;
            float scoreTotal = 0;

            var scoreall = _context.Scores
                .Where(r => r.Title == id_recipe.ToString()).ToList();

            for (int i = 0; i < scoreall.Count(); i++)
            {
                sumScore += scoreall[i].ScorePoints;
            }
            scoreTotal = sumScore / scoreall.Count();

            return scoreTotal;
        }

        public string Load(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
