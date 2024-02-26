using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages
{
    public class FirstViewModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public FirstViewModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public List<string> rpicto { get; set; } = new List<string>();
        public List<float> rscore { get; set; } = new List<float>();
        public List<string> tags { get; set; } = new List<string>();
        List<List<string>> listOfLists = new List<List<string>>();

        public async Task<IActionResult> OnGetAsync(string? search)
        {
            string[] strSplitSearch = null;
            List<RecipeInfo> iRecipe = new List<RecipeInfo>();

            if (search != null) //busqueda por titulo
            {
                RecipeInfo = await _context.RecipeInfos
                    .Where(r => r.RecipeTitle.Contains(search)).ToListAsync();

                if (RecipeInfo.Count <= 0) //busqueda por ingrediente
                {
                    strSplitSearch = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    
                    //var anyluck = strSplitSearch.Any(s => search.Contains(s));

                    foreach (var ingredient in strSplitSearch)
                    {
                        //var query = _context.RecipeIngredients
                        var query = _context.RecipeIngredients
                            .Where(i => i.IngredientName.Contains(ingredient))
                            .Join(_context.RecipeInfos,
                            i => i.RecipeId,
                            r => r.RecipeId,
                            (r, i) => new RecipeInfo
                            {
                                RecipeId = r.RecipeId,
                                RecipeTitle = r.Recipe.RecipeTitle,
                                RecipePic = r.Recipe.RecipePic,
                                RecipeTags = r.Recipe.RecipeTags
                            }).ToList();


                        for (int i = 0; i < query.Count; i++)
                        {
                            iRecipe.Add(query[i]);
                        }
                        
                    }

                    if (iRecipe.Count > 0)
                    {
                        RecipeInfo = iRecipe.GroupBy(r => r.RecipeTitle)
                            .Where(r => r.Count() != 0)
                            .Select(r => r.First()).ToList();

                        foreach (var recipe in RecipeInfo)
                        {
                            rscore.Add(GetRecipeRating(recipe.RecipeId));
                            
                            if (recipe.RecipePic != null)
                            {
                                rpicto.Add(Load(recipe.RecipePic));
                            }
                            else
                            {
                                rpicto.Add("tortilla-basic-cuadro.jpg");
                            }

                            var recipeTags = await _context.RecipeTags.
                                Where(t => t.RecipeId == recipe.RecipeId).ToListAsync();

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

                    }
                }
                else
                {
                    foreach (var recipe in RecipeInfo)
                    {
                        rscore.Add(GetRecipeRating(recipe.RecipeId));

                        if (recipe.RecipePic != null)
                        {
                            rpicto.Add(Load(recipe.RecipePic));
                        }
                        else
                        {
                            rpicto.Add("tortilla-basic-cuadro.jpg");
                        }
                        var recipeTags = await _context.RecipeTags.
                             Where(t => t.RecipeId == recipe.RecipeId).ToListAsync();

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

        /*public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                    return true;
            }

            return false;
        }*/
    }
}
