using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Tortillapp_web.tortilla;
using Tortillapp_web.Pages.Users;

namespace Tortillapp_web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
		public string umail { get; set; }
		[BindProperty]
		public string upass { get; set; }
		public string merror { get; set; }

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

        //public async Task<IActionResult> OnPostAsync()
        public IActionResult OnPost()
		{
            //nConnect = new CConnection();¿
            //nConnect.loginUser(umail,upass);
            using (var context = new tortillaContext())
            {
                try
                {
                    //context.Database.EnsureCreated();
                    if (!ModelState.IsValid)
                    {
                        //var user = context.UserData;
                        merror = umail.ToString();
                        //var umodel = new UserModel(context);
						
						/*(this.UserModel.UserMail, this.UserModel.UserPass);
						//foreach (var u in umodel.UserData)
                        //{
                            if (umail.)
                            {
                                if (upass.Equals(u.UserPass))
                                {
                                   
                                    return RedirectToPage("MyProfile");
                                }
                            }
                            else
                            {
                                merror = "El password o la contraseña son incorrectos";
                                return Page();
                            }
                        //}*/
                    }
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
        }
    }
}
