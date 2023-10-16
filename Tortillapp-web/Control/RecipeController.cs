using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core.Metadata.Edm;

namespace Tortillapp_web.Control
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /*public IActionResult _RecipePartial()
        {
            return PartialView();
        }*/

    }
}
