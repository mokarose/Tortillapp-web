using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Tortillapp_web.Model;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web.Control
{
    public class UserController : Controller
    {
        private IWebHostEnvironment environment;

        public UserController(IWebHostEnvironment _enviroment)
        {
            environment = _enviroment;
        }
        public UserData UserData { get; set; } = default!;

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
 