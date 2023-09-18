using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Admin
{
    public class ActualUsers : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public ActualUsers(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public UserData User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id  == null || _context.UserDatas == null)
            {
                return NotFound();
            }

            var user = await _context.UserDatas.FindAsync(id);
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
