using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using Org.BouncyCastle.Asn1.Ocsp;
//using System.Web

namespace Tortillapp_web.Pages
{
    public class MyProfileModel : PageModel
    {
        private CConnection nConnect;
        public string uname => (string)TempData[nameof(uname)];
        public string ushow => (string)TempData[nameof(ushow)];
        public string umail => (string)TempData[nameof(umail)];
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void OnGet()
        {
                       
            try
            {
                //if (!Page.IsPostBack)
                nConnect = new CConnection();
                MySqlDataReader data = null;
                MySqlCommand cmd = new MySqlCommand();
                string usname = "";
                string usmail = "";
                cmd.Connection = nConnect.getConnection();
                cmd.CommandText = "select * from user_data where (user_name='admin')";
                //Console.WriteLine(cmd.Connection.State);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    //Console.WriteLine(data.GetString("user_name"));
                    usname = data.GetString("user_name");
                    usmail = data.GetString("user_mail");
                }
                TempData[nameof(uname)] = usname;
                TempData[nameof(umail)] = usmail;
            }
            catch (Exception ex)
            {
                this.RedirectToPage(ex.Message);
            }

        }

        public IActionResult OnPost([FromForm]string uname)
        {
            return RedirectToPage("MyProfile");
        }
    }
}
