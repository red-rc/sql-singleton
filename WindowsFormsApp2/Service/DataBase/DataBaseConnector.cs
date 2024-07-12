using MySql.Data.MySqlClient;

namespace WindowsFormsApp2.Service.DataBase
{
    public class DataBaseConnector
    {
        public static MySqlConnection getConnection(
            string host = "localhost",
            int port = 3306,
            string dbName = "",
            string userName = "root",
            string password = "root")
        {
            string param = $"server={host};port={port};uid={userName};pwd={password};database={dbName};";

            MySqlConnection connection = new MySqlConnection(param);

            return connection;
        }
    }
}