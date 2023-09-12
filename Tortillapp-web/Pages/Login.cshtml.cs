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
            /*try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    this.RedirectToPage("Index");
                }
            }
            catch (Exception ex)
            {
                merror = ex.Message;
            }
            this.Page();*/
        }

        public void OnPost()
        {

        }
        /*public IActionResult OnPost()
		{
            //nConnect = new CConnection();
            //nConnect.loginUser(umail,upass);
            using (var context = new tortillaContext())
            {
                try
                {
                    //context.Database.EnsureCreated();
                    if (ModelState.IsValid)
                    {
                        var userMod = context.UserData;

                        //foreach (var u in userMod)
                        //{
                        //if (umail.Equals(u.UserMail))
                        if (umail.Equals(userMod. User.UserName))
                            {
                                //if (upass.Equals(u.UserPass))
                                if (upass.Equals(User.UserPass))
                                {
                                    return RedirectToPage("MyProfile", User);
                                }
                            }
                            else
                            {
                                merror = "La contraseña o el correo son incorrectos";
                                return Page();
                            }
                        }
                    //}
                    else
                    {
                        return Page();
                    }
                    
                }
                catch (Exception ex)
                {
                    return RedirectToPage("Error", ex);//merror = ex.Message;
				}
            }
            return Page();
        }*/
        
    }
}
