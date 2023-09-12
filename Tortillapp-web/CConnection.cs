using MySql.Data.MySqlClient;
using Tortillapp_web.Pages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Common;
using Tortillapp_web.Model;

namespace Tortillapp_web
{
	internal class CConnection
    {
		public MySqlConnection getConnection()
        {
            MySqlConnection conexion = new MySqlConnection("Server=localhost;Port=3306;Database=tortilla;User ID=root;Password=Lord0Rings");
            //string conexion = ConfigurationManager.connectionStrings["conexMySQL"].connectionString;
            conexion.Open();

            return conexion;
        }

        public async Task<List<LoginModel>> LoginMethodAsync(string userMail, string userPass)
        {
            List<LoginModel> login = new List<LoginModel>();
            try
            {
                MySqlParameter mailParam = new MySqlParameter("@user_mail", userMail ?? (object)DBNull.Value);
                MySqlParameter passParam = new MySqlParameter("@user_pass", userPass ?? (object)DBNull.Value);
            
                string mysqlQuery = "select * from user_data where (user_name=@user and user_pass=@pass)";

                login = await this.Query<LoginModel>().FromMySql(mysqlQuery,userMail,userPass).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return login;
        }

    }
}
