using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Tortillapp_web.Model;
using System.Drawing;
using System.Buffers.Text;

namespace Tortillapp_web.Pages.Users
{
    public class MyProfileModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;
        private IWebHostEnvironment _environment;

        public MyProfileModel(Tortillapp_web.Data.tortillaContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        //public string uname { get; set; } //usuario
        //public string ushow { get; set; } //Nombre para mostrar
        //public string umail { get; set; } //Correo
        public string upass { get; set; } //Contraseña
        public string xpass { get; set; }
        //public byte[]? picto { get; set; } //Imagen
        [BindProperty]
        public string image { get; set; } //Imagen
        public string merror { get; set; }

        [BindProperty]
        public UserData User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string iUser = HttpContext.Session.GetString("Usuario"); //Usuario actual
            
            if (iUser == null) //|| _context.UserDatas == null)
            { 
                return NotFound();
            }

            var user = await _context.UserDatas.FirstOrDefaultAsync(u => u.UserName == iUser);

            if (user == null)
            {
                return NotFound();
            }
            
            User = user;

            image = Encoding.UTF8.GetString(User.ShowPic);//_context.UserDatas.FirstOrDefault(i => i.ShowPic.Equals(User.ShowPic))?.ShowPic;
            //Al leer el valor está vacío
            //image = Load(imagen);
            
            return Page();
        }

        /*public IActionResult OpPostUploadImage()
        {
            //byte[] image = Encoding.ASCII.GetBytes(picto);

            return RedirectToPage("MyProfile");
        }*/

        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
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

            if(image != null)
            {
                User.ShowPic = Upload(image);
            }

            var userToUpdate = await _context.UserDatas.FindAsync(User.UserId);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            User.RoleId = userToUpdate.RoleId;
            User.RemenberMe = userToUpdate.RemenberMe;
            User.UserCreated = userToUpdate.UserCreated;
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

        public byte[] Upload(IFormFile image)
        {
            string wwwPath = this._environment.WebRootPath;
            byte[] data = null;
            //string contentPath = this._environment.ContentRootPath;

            string path = Path.Combine(wwwPath, "pics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string imageUpload = null;
            string filename = Path.GetFileName(image.FileName);

            using (FileStream file = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    image.CopyTo(memory);
                    //data = new byte[stream.Length];//stream.GetBuffer();
                    file.Write(memory.ToArray());
                    data = memory.ToArray();
                    //imageUpload = filename;
                    //byte[] buffer = new byte[1024];
                    //stream.Read(buffer, 0, (byte)image.Length);
                }
            }
            return data;
        }

        public string Load(byte[] data)
        {
            string image = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                image = Image.FromStream(stream).ToString();
            }

            return image;
        }
    }
}
