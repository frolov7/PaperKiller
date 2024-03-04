using Microsoft.Extensions.Options;
using PaperKiller.Utils;
using System.Data.SqlClient;

namespace PaperKiller.Repository
{
    public class Connector
    {
        private SqlConnection connection;
        private readonly string _connectionString;

        public Connector(IOptions<AppSettings> appSettings)
        {
            _connectionString = appSettings.Value.MyDatabaseConnection;

            try
            {
                connection = new SqlConnection(_connectionString);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
                throw;
            }
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public List<object[]> ExecuteSelect(string cmdTxt, Dictionary<string, object> parameters = null)
        {
            List<object[]> result = new List<object[]>();

            try
            {
                using (var command = new SqlCommand(cmdTxt, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                        }
                    }

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        int columnCount = reader.FieldCount;

                        while (reader.Read())
                        {
                            object[] row = new object[columnCount];
                            reader.GetValues(row);
                            result.Add(row);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        public int ExecuteNonQuery(string cmdTxt, Dictionary<string, object> parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                using (var command = new SqlCommand(cmdTxt, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                        }
                    }

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                System.Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected;
        }
    }
}
