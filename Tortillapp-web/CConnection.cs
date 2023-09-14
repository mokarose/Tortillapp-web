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

    }
}
