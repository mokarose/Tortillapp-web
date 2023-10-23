using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize("Usuario")]
        public IActionResult Index(UserData u)
        {
            return View(u);
        }
    }
}
 