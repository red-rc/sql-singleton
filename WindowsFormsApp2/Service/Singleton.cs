using MySql.Data.MySqlClient;
using WindowsFormsApp2.Service.DataBase;

namespace WindowsFormsApp2.Service
{
    public class Singleton
    {
        private MySqlConnection connection;
        private Singleton() { }

        private static Singleton instance;

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public void setConnection(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public MySqlConnection getConnection(string dbName = "store")
        {
            if (this.connection == null)
            {
                this.connection = DataBaseConnector.getConnection(dbName: dbName);
            }

            return this.connection;
        }
    }
}
