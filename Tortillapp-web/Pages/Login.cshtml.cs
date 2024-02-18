using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tortillapp_web.Data;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public LoginModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Se requiere el correo")]
        [Display(Name = "Correo")]
        public string umail { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Se requiere la contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string upass { get; set; }
        public bool reme { get; set; } 
        public string? merror { get; set; }

        public UserData User { get; set; } = default!;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            var isLoged = Login(umail, upass);
            if (isLoged == null)
            {
                merror = "La contraseña o el correo son incorrectos";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("Usuario", isLoged.UserName);
                if (isLoged.ShowPic != null)
                {
                    HttpContext.Session.SetString("Imagen", Encoding.UTF8.GetString(isLoged.ShowPic));
                }
                else
                {
                    HttpContext.Session.SetString("Imagen", "profile3.png");
                }
                return RedirectToPage("MyProfile");
            }
        }

        public UserData Login(string iUseMail, string iUserPass)
        {
            var user = _context.UserData.SingleOrDefault(u => u.UserMail.Equals(iUseMail));
            
            if (user != null)
            {
                if (user.UserPass.Equals(EcryptPass(iUserPass)))
                {
                    return user;
                }
            }
            return null;
        }

        public string EcryptPass(string password)
        {
            string result = null;
            byte[] encrypt = System.Text.Encoding.Unicode.GetBytes(password);
            result = Convert.ToBase64String(encrypt);
            return result;
        }
    }
}
