using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tortillapp_web.Models;

namespace Tortillapp_web.Pages.Admin
{
    public class ViewUsersModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public ViewUsersModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public UserData User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(UserData iUser)
        {
            if (iUser == null || _context.UserData == null)
            {
                return NotFound();
            }

            var user = await _context.UserData.FindAsync(iUser.UserId);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}

