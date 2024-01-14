using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Users
{
    public class RegisterModel : PageModel
    {
		private readonly Tortillapp_web.Data.tortillaContext _context;
        private IWebHostEnvironment _environment;

        public RegisterModel(Tortillapp_web.Data.tortillaContext context, IWebHostEnvironment environment)
		{
			_context = context;
            _environment = environment;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
        [Required(ErrorMessage = "¡Falta el correo!")]
        [Display(Name = "Correo")]
        public string umail { get; set; }
		[BindProperty]
        [Required(ErrorMessage = "¡Falta el nombre de usuario!")]
        [Display(Name = "Usuario")]
        public string uname { get; set; }
		[BindProperty]
        public string? pass1 { get; set; }
		[BindProperty]
        public string? pass2 { get; set; }
        public byte[] image { get; set; }
        public string? merror { get; set; }

        public UserData User { get; set; } = default!;

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
		{
            byte[]? bytes = null;

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
				string epass = EcryptPass(pass1);

				bool mexists = ExistMail(umail);
				if (mexists)
				{
					merror = "El correo agregado ya existe, prueba con otro";
					return Page();
				}

                 bytes = AddImage();
                
                _context.UserDatas.Add(new UserData
                {
                    UserMail = umail,
                    UserName = uname,
					UserPass = epass,
                    ShowPic = bytes,
                    RoleId = 3,
					UserCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }
			
            ushort last_insert = _context.UserDatas.Max(r => r.UserId);

			return Redirect("/Success?id=" + last_insert + "&type=registro");
		}

		public bool ExistMail(string email)
		{
            if(_context.UserDatas.FirstOrDefault(u => u.UserMail == email) == null)
			{
				return false;
			}
			return true;
		}

		public string EcryptPass(string password)
		{
			string result = null;
			byte[] encrypt = System.Text.Encoding.Unicode.GetBytes(password);
			result = Convert.ToBase64String(encrypt);
			return result;
		}

        public byte[] AddImage()
        {
            string wwwPath = this._environment.WebRootPath;
            byte[] data = null;

            string path = Path.Combine(wwwPath, "pics");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filename = Path.GetFileName("profile1.png");

            bool exists = System.IO.File.Exists(Path.Combine(path, filename));
            if (exists)
            {
                using (FileStream file = new FileStream(Path.Combine(path, filename), FileMode.Append))
                {
                    using (MemoryStream memory = new MemoryStream())
                    {
                        file.Write(memory.ToArray());
                        data = Encoding.ASCII.GetBytes(filename);
                    }
                }
            }
            else
            {
                TempData["merror"] = "Hay un problema para agregar la imagen";
            }

            return data;
        }
    }
}
