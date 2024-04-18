using MySql.Data.MySqlClient;

namespace SamartTradeBackend
{
    public class Conexion
    {
        private string server = "bezz64pmlgkdtkejch0i-mysql.services.clever-cloud.com";
        private string database = "bezz64pmlgkdtkejch0i";
        private string user = "uxri6to3ohabhczv";
        private string password = "Vfg8AwmWlxQB3TrLuJoF";
        private string port = "3306";
        private string cadenaConexion;

        public Conexion()
        {
            cadenaConexion = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};";
        }

        public MySqlConnection GetConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
