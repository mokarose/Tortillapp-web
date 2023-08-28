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

        public MySqlDataReader readUser()
        {
            return null;
        }
    }
}
