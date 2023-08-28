using MySql.Data.MySqlClient;
using Tortillapp_web.Pages;

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

        public bool loginUser(string user, string pass)
        {
            MySqlDataReader data = null;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from user_data where (user_name=@user and user_pass=@pass)";
            data = cmd.ExecuteReader();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    UserCache.iuser = data.GetInt32("user_id");
                    UserCache.uname = data.GetString("user_name");
                    UserCache.umail = data.GetString("user_mail");
                    UserCache.ushow = data.GetString("user_show");
                    UserCache.upass = data.GetString("user_pass");
                }
                return true;
            }
            else
                return false;
            
        }

    }
}
