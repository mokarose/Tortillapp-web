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

        [BindProperty]
        public UserData User { get; set; } = default!;
        public string upass { get; set; }
        public string xpass { get; set; }

        public async Task<IActionResult> OnGetAsync(UserData iUser)
        {
                       
            if (iUser == null || _context.UserDatas == null) { 
                return NotFound();
            }

            var user = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserId == iUser.UserId);

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
            if (iUser == null || _context.UserDatas == null)
            {
                return NotFound();
            }

            var user = await _context.UserDatas.FindAsync(iUser.UserId);

            if (user != null)
            {
                User = user;
                _context.UserDatas.Update(User);
                await _context.SaveChangesAsync();  
            }
            return Page();
        }
    }
}
