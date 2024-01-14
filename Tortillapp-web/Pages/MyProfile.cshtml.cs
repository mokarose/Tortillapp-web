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
using NuGet.Packaging;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

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
        [BindProperty]
        public string upass { get; set; } //Contraseña
        [BindProperty]
        public string xpass { get; set; }
        //public byte[]? picto { get; set; } //Imagen byte
        public IFormFile image { get; set; }
        public string picto { get; set; } //Imagen
        [TempData]
        public string merror { get; set; }
        [TempData]
        public string message { get; set; }

        [BindProperty]
        public UserData User { get; set; } = default!;

        [HttpGet]
        [Authorize("Usuario")]
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

            //image = Encoding.UTF8.GetString(User.ShowPic);
            if (User.ShowPic != null)
            {
                picto = Load(User.ShowPic);
            }
            
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            byte[]? bytes = null;

            if (upass != null)
            {
                if (upass.Equals(xpass))
                {
                    User.UserPass = upass;
                }
                else
                {
                    TempData["merror"] = "Las contraseñas no coinciden";
                    return RedirectToPage("MyProfile");
                }
            }

            var userToUpdate = await _context.UserDatas.FindAsync(User.UserId);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (image != null)
            {
                bytes = Upload(image);
                if (bytes != null)
                {
                    if (userToUpdate.ShowPic != null) {
                        Delete(Load(userToUpdate.ShowPic));
                    }
                    User.ShowPic = bytes;
                }
            }
            else
            {
                User.ShowPic = userToUpdate.ShowPic;
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
                TempData["message"] = "Información actualizada";
                return RedirectToPage("MyProfile");
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
            string filepath = null;
            //string contentPath = this._environment.ContentRootPath;

            string path = Path.Combine(wwwPath, "pics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string imageUpload = null;
            string filename = Path.GetFileName(image.FileName);
            //System.IO.File.Move(filename, filename.GetHashCode().ToString());

            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                if (stream.Length <= 2097152)
                {
                    image.CopyTo(stream);
                }
                else
                {
                    TempData["merror"] = "El archivo es muy grande (2MB max)";
                }
            }

            bool exists = System.IO.File.Exists(Path.Combine(path, filename));
            if (exists)
            {
                filepath = System.Guid.NewGuid().ToString();
                System.IO.File.Move(Path.Combine(path, filename), Path.Combine(path, Path.ChangeExtension(filepath, ".jpg")));
                System.IO.File.Delete(Path.Combine(path, filename));
            }
            else
            {
                TempData["merror"] = "Hay un problema para copiar la imagen";
            }

            using (FileStream file = new FileStream(Path.Combine(path, Path.ChangeExtension(filepath, ".jpg")), FileMode.Create))
            {
                image.CopyTo(file);

                using (MemoryStream memory = new MemoryStream())
                {
                    file.Write(memory.ToArray());
                    //filepath = file.Name;
                    data = Encoding.ASCII.GetBytes(Path.ChangeExtension(filepath, ".jpg"));
                    //byte[] buffer = new byte[1024];
                    //stream.Read(buffer, 0, (byte)image.Length);
                }
            }
            return data;
        }

        public string Load(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public void Delete(string filename)
        {
            string wwwPath = this._environment.WebRootPath;
            string path = Path.Combine(wwwPath, "pics");
            System.IO.File.Delete(Path.Combine(path, filename));
        }
    }
}
