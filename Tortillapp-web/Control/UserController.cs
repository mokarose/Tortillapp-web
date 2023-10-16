using Microsoft.AspNetCore.Mvc;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index(UserData u)
        {
            return View(u);
        }
    }
}
 