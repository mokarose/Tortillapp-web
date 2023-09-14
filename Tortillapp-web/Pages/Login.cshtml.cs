using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Tortillapp_web.Data;
using Tortillapp_web.Model;
using Tortillapp_web.Control;

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
        public string umail { get; set; }
		[BindProperty]
		public string upass { get; set; }
        public bool reme { get; set; } 
        public string? merror { get; set; }

        public UserData User { get; set; } = default!;
        public IActionResult OnGet()
        {
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
                return RedirectToPage("MyProfile", isLoged);
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
