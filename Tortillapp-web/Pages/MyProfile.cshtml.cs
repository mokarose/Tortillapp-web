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

        public string uname { get; set; } //usuario
        public string ushow { get; set; } //Nombre para mostrar
        public string umail { get; set; } //Correo
        public string upass { get; set; } //Contraseña
        public string xpass { get; set; }
        public byte[]? picto { get; set; } //Imagen
        public string merror { get; set; }

        [BindProperty]
        public UserData User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(UserData iUser)
        { 
            if (iUser == null) //|| _context.UserDatas == null)
            { 
                return NotFound();
            }

            var user = await _context.UserDatas.FindAsync(iUser.UserId);//FirstOrDefaultAsync(u => u.UserId == iUser.UserId);

            if (user == null)
            {
                return NotFound();
            }
            
            User = user;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(UserData iUser)
        {
            var userToUpdate = await _context.UserDatas.FindAsync(iUser.UserId);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (upass != null)
            {
                if (upass.Equals(xpass))
                {
                    User.UserPass = upass;
                }
                else
                {
                    merror = "Las contraseñas no coinciden";
                }
            }

            User.RoleId = iUser.RoleId;
            User.RemenberMe = iUser.RemenberMe;
            User.UserCreated = iUser.UserCreated;
            User.LastUpdated = DateTime.Now;

            _context.Entry(userToUpdate).CurrentValues.SetValues(User);
            _context.Entry(userToUpdate).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                return Page();
            }
            catch (Exception ex)
            {
                merror = ex.Message;
            }

            return Page();
        }
    }
}
