using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class UserController : Controller
    {
        private IWebHostEnvironment environment;

        public UserController(IWebHostEnvironment _enviroment)
        {
            environment = _enviroment;
        }

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

        [HttpPost]
        public IActionResult Upload(IFormFile image)
        {
            string wwwPath = this.environment.WebRootPath;
            string contentPath = this.environment.ContentRootPath;

            string path = Path.Combine(wwwPath, "pics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string imageUpload = null;
            string filename = Path.GetFileName(image.FileName);

            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                image.CopyTo(stream);
                imageUpload = filename;
                ViewBag.Message = filename;
            }

            return View();
        }
    }
}
 