using System;
using MySql.Data.MySqlClient;

namespace Db.Mysql
{
    public class DbConnect:IDisposable
    {
        // Database connection details
        // Database hosting server ip/name
        private string host = "localhost";
        // Database user name
        private string userId = "root";
        // Database password
        private string password = "1qaz2wsx@";
        // Database server port no
        private string portNo = "3306";
        // Database name
        private string database = "ntbkidscard";

        // Mysql connection reference
        private MySqlConnection connection;

        // Mysql command reference
        private MySqlCommand command;

        // Initialize the MySql database connection object
        public DbConnect()
        {
            connection = new MySqlConnection
            {
                ConnectionString = "server=" + host
                + ";user id=" + userId
                + ";password=" + password
                + ";persistsecurityinfo=True;port=" + portNo
                + ";database=" + database
                + ";SslMode=None"
            };
        }
        // Execute the data retreiving query
        // Run CloseConnection() method using this method
        public MySqlDataReader MysqlExecuteQuery(string query)
        {
            // open connection with database
            OpenConnection();
            // Execute the query
            command = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = command.ExecuteReader();
            // Return the datareader to fetch the data
            return dataReader;
        }
        // Execute the data storing query
        public int MysqlExecuteNonQuery(string query)
        {
            int numberOfRow = 0;
            OpenConnection();
            // Execute the query
            command = new MySqlCommand(query, connection);
            numberOfRow = command.ExecuteNonQuery();
            // Close the connection between application and database
            CloseConnection();
            // Return the number of rows updated by execution
            return numberOfRow;
        }
        // Open the connection which is initialze in the constructor
        private void OpenConnection()
        {
            // Open the connection between application and database
            if (connection != null)
                connection.Open();
        }
        // Close the connection which is opened
        private void CloseConnection()
        {
            // Close the connection between application and database
            if (connection != null)
                connection.Close();
        }        
        public void Dispose()
        {
            // Close the connection when disposing the DbConnect object
            CloseConnection();           
        }
    }
}