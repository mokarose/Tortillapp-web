using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Users
{
    public class RegisterModel : PageModel
    {
		private readonly Tortillapp_web.Data.tortillaContext _context;

		public RegisterModel(Tortillapp_web.Data.tortillaContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public string umail { get; set; }
		[BindProperty]
		public string uname { get; set; }
		[BindProperty]
        public string? pass1 { get; set; }
		[BindProperty]
        public string? pass2 { get; set; }
        public string? merror { get; set; }

        public UserData User { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
		{
            if (!ModelState.IsValid)// || _context.UserDatas == null || User == null)
			{
				return Page();
			}

			if (!pass1.Equals(pass2))
			{
				merror = "Las contraseñas no coinciden";
                return Page();
            }
			else
			{
                _context.UserDatas.Add(new UserData
                {
                    UserMail = umail,
                    UserName = uname,
					UserPass = pass1,
                    RoleId = 3,
					UserCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

			return RedirectToPage("Index");
		}
	}
}
