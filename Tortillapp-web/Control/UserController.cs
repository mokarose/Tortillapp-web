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

        public IActionResult SendMail(UserData user)
        {
            MailMessage rmail = new MailMessage();
            rmail.From = new MailAddress("notificaciones@tortillapp.com.mx", "Tortillapp", System.Text.Encoding.UTF8);
            rmail.To.Add(user.UserMail);
            rmail.Subject = "Correo de prueba";
            rmail.Body = "Este es un correo de prueba";
            rmail.IsBodyHtml = true;
            rmail.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = true;
            smtp.Host = "smtpout.secureserver.net";
            smtp.Port = 465;
            smtp.Credentials = new System.Net.NetworkCredential("notificaciones@tortillapp.com.mx", "#Pulpot4k0");
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            smtp.Send(rmail);

            return RedirectToPage("Success");
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
 