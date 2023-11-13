using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Tortillapp_web.Control;
using Tortillapp_web.Model;
using static System.Net.WebRequestMethods;

namespace Tortillapp_web.Pages
{
    public class SuccessModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public SuccessModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public string utype { get; set; }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync(int id, string type)
        {
            var user = _context.UserDatas.FirstOrDefault(u => u.UserId == id);
            utype = type;

            if (id != null)
            {
                if (type == "registro")
                {
                    SentMailRegister(user);
                }
                else
                {
                    SentMailUpdate(user);
                }
            }

            return Page();
        }

        public void SentMailRegister(UserData user)
        {
            MailMessage rmail = new MailMessage();
            ///rmail.From = new MailAddress("notificaciones@tortillapp.com.mx", "Tortillapp", System.Text.Encoding.UTF8);
            rmail.From = new MailAddress("rosario_3b2_eab@yahoo.com.mx", "Tortillapp", System.Text.Encoding.UTF8);
            rmail.To.Add(user.UserMail);
            rmail.Subject = "Correo de prueba";
            rmail.Body = "Este es un correo de prueba";
            rmail.IsBodyHtml = true;
            rmail.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            ///smtp.Host = "smtpout.secureserver.net"; //GoDaddy mail
            ///smtp.Host = "smtp.office365.com";
            smtp.Host = "smtp.mail.yahoo.com";
            ///smtp.Port = 465;
            ///smtp.Port = 587;
            smtp.Port = 587;
            ///smtp.Credentials = new System.Net.NetworkCredential("notificaciones@tortillapp.com.mx", "#Pulpot4k0");
            ///smtp.Credentials = new System.Net.NetworkCredential("mary_chayo_@hormail.com", "130619961d","outlook.com");
            smtp.Credentials = new System.Net.NetworkCredential("rosario_3b2_eab@yahoo.com.mx", "ispfwrmuubvyjpgx");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;

            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            smtp.Send(rmail);
        }

        public void SentMailUpdate(UserData user)
        {
            
        }
    }
}
