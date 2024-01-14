using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core.Metadata.Edm;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(RecipeIngredient ingredient)
        {
            return View(ingredient);
        }

        public IActionResult LikeRecipe(RecipeInfo recipeInfo)
        {
            return View(recipeInfo);
        }


    }
}
