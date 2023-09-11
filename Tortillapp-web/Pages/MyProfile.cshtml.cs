using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Users
{
    public class MyProfileModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public MyProfileModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }
        //private CConnection nConnect;
        [BindProperty]
        public UserData User { get; set; } = default!;
        public string upass { get; set; }
        public string xpass { get; set; }
        //public string uname => (string)TempData[nameof(uname)];
        //public string ushow => (string)TempData[nameof(ushow)];
        //public string umail => (string)TempData[nameof(umail)];

        public async Task<IActionResult> OnGetAsync(UserData iUser)
        {
                       
            if (iUser == null || _context.UserData == null) { 
                return NotFound();
            }

            var user = await _context.UserData.FirstOrDefaultAsync(u => u.UserId == iUser.UserId);

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

        //public IActionResult OnPost([FromForm]string uname)
        public async Task<IActionResult> OnPostAsync(UserData iUser)
        {
            if (iUser == null || _context.UserData == null)
            {
                return NotFound();
            }

            var user = await _context.UserData.FindAsync(iUser.UserId);

            if (user != null)
            {
                User = user;
                _context.UserData.Update(User);
                await _context.SaveChangesAsync();  
            }
            return Page();
        }
    }
}
