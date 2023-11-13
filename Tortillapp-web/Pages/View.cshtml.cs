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

namespace Tortillapp_web.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public RecipeModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }
        public RecipeInfo RecipeInfo { get; set; } = default!;
        public IList<RecipeStep> Step { get; set; } = default!;
        public IList<RecipeIngredient> Ingredient { get; set; } = default!;
        public IList<RecipeTag> Tag { get; set; } = default!;
        public List<Score> AllScore { get; set; } = default!;
        public UserRating Score { get; set; } = default!;
        public UserData User { get; set; } = default!;
        public UserData UserLogged { get; set; } = default!;
        public string picRecipe { get; set; }
        public string picUser { get; set; }
        public string userShow { get; set; } 
        public string picUserLog { get; set; }
        [BindProperty]
        public string userShowLog { get; set; }
        [BindProperty]
        public ushort idRecipe { get; set; }
        [BindProperty]
        public ushort uscore { get; set; }
        [BindProperty]
        public string ucomment {  get; set; }
        public float rscore {  get; set; }

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
            Tag = await _context.RecipeTags
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
            }

            string iUser = HttpContext.Session.GetString("Usuario");
            var userlogged = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserName.Equals(iUser));

            if (userlogged != null)
            {
                UserLogged = userlogged;

                if (UserLogged.ShowPic != null)
                {
                    picUserLog = Load(UserLogged.ShowPic);
                }
                else
                {
                    picUserLog = "profile3.png";
                }

                if (UserLogged.ShowName != null)
                {
                    userShowLog = UserLogged.ShowName;
                }
                else
                {
                    userShowLog = UserLogged.UserName;
                }
            }
            else
            {
                picUserLog = "profile3.png";
                userShowLog = "Usuario";
            }

            return Page();
        }

        [HttpPost]
        public IActionResult RecipeLikePost()
        {
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (userShowLog.Equals("Usuario"))
            {
                return RedirectToPage("Login");
            }

            if (uscore > 0 && uscore < 5)
            {
                var userComment = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserName.Equals(userShowLog));

                if (userComment != null)
                {
                    _context.Scores.Add(new Score
                    {
                        ScorePoints = uscore,
                        Title = idRecipe.ToString(),
                        Comment = ucomment,
                        ScoreStatus = 1,
                        ScoreModified = DateTime.Now

                    });
                }
                else
                {
                    _context.Scores.Add(new Score
                    {
                        ScorePoints = uscore,
                        Title = idRecipe.ToString(),
                        Comment = "",
                        ScoreStatus = 1,
                        ScoreModified = DateTime.Now

                    });
                }
                await _context.SaveChangesAsync();

                ushort last_insert = _context.Scores.Max(r => r.ScoreId);

                UpdateRating(idRecipe, userComment.UserId, last_insert);

                return Redirect("View?id=" + idRecipe);
            }
            return Redirect("View?id=" + idRecipe);
        }

        public void UpdateRating(ushort id_recipe, ushort id_user, ushort id_score)
        {
            //var scorerating = _context.UserRatings.FirstOrDefault(s => s.RecipeId == id_recipe);

            //if (scorerating != null)
            //{
                _context.UserRatings.Add(new UserRating
                {
                    RecipeId = id_recipe,
                    UserId = id_user,
                    UserScore = id_score,
                    ScoreAdded = DateTime.Now
                });
                _context.SaveChanges();
            //}
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
