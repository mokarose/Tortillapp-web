using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public ResetPasswordModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string npass { get; set; }
        [BindProperty]
        public string mpass { get; set; }
        [BindProperty]
        public ushort uid { get; set; }
        public string uname => (string)TempData[nameof(uname)];
        public string merror { get; set; }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserId == id);
            TempData[nameof(uname)] = user.UserName;
            uid = user.UserId;

            return Page();
        }

        [HttpPost]
        public IActionResult OnPostBackToIndex(string btnCancel)
        {
            if (btnCancel.Equals("Atrás"))
            {
                return RedirectToPage("/Reset");
            }
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { 
                return Page();
            }

            if (!npass.Equals(mpass))
            {
                merror = "Las contraseñas no coinciden";
                return Page();
            }
            
            string epass = EcryptPass(npass);
            var user = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserId == uid);

            user.UserPass = epass;

            await _context.SaveChangesAsync();
            
            return Redirect("/Success?id=" + uid + "&type=actualizacion");
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
