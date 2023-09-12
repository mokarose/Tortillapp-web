using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Tortillapp_web.Data;
using Tortillapp_web.Model;
using Tortillapp_web.Control;

namespace Tortillapp_web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string umail { get; set; }
		[BindProperty]
		public string upass { get; set; }
        public bool reme { get; set; } 
        public string? merror { get; set; }

        public UserData User { get; set; } = default!;
        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}
