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
		public UserData User { get; set; } = default!;
		public UserRole Role { get; set; } = default!;
        public string? pass1 { get; set; }
        public string? pass2 { get; set; }


        public async Task<IActionResult> OnPostAsync()
		{
			var role = _context.UserRoles.ToList();
			//ViewData.GetViewDataInfo() = role;
			//Encriptar password

            if (!ModelState.IsValid || _context.UserDatas == null || User == null)
			{
				return Page();
			}

			_context.UserDatas.Add(User);
			await _context.SaveChangesAsync();

			return RedirectToPage("/MyProfile");
		}
	}
}
