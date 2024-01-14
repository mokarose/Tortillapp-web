using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Tortillapp_web.Pages
{
    public class ResetModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public ResetModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string umail { get; set; }
        [TempData]
        public string message { get; set; }
        public string merror { get; set; }
        public void OnGet()
        {
        }

        [HttpPost]
        public IActionResult OnPostBackToIndex(string btnCancel)
        {
            if (btnCancel.Equals("Cancelar"))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Agrega tu correo para continuar";
                return Page();
            }

            var user = _context.UserDatas.FirstOrDefault(u => u.UserMail.Equals(umail));

            if (user != null)
            {
                return Redirect("/ResetPassword?id=" + user.UserId);
            }
            else
            {
                merror = "El correo ingresado no existe";
            }

            return Page();
        }
    }
}
