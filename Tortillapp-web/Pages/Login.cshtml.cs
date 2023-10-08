using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

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
                return RedirectToPage("MyProfile");
            }
        }

        public UserData Login(string iUseMail, string iUserPass)
        {
            var user = _context.UserDatas.SingleOrDefault(u => u.UserMail.Equals(iUseMail));
            if (user != null)
            {
                var pass = _context.UserDatas.SingleOrDefault(u => u.UserPass.Equals(iUserPass));
                if (pass != null)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
