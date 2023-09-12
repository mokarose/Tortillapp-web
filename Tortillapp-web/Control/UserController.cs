using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Model.UserData user)
        {
            if(ModelState.IsValid)
            {
                if(user.UserMail.Equals(user.UserMail))
                {
                    if(user.UserPass.Equals(user.UserPass))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "La contraseña o el correo son incorrectos");
                    }
                }
            }
            return View(user);
        }
        public ActionResult Logout() 
        {
            return RedirectToAction("Index");
        }
    }
}
