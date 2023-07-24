using System.Data.Common;
using System.Data;
using Detailing.Interfaces;
using MySql.Data.MySqlClient;

namespace Detailing.Services
{
    public class MySqlDatabaseService : IDatabaseService
    {
        public string ConnectionString { get; set; }

        public int ExecuteNonQueryStoredProcedure(string storedProcedureName, IDbDataParameter[] parameters)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    try
                    {
                        Console.WriteLine("Openning sql connection");
                        connection.Open();
                        var result = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Command `{storedProcedureName}` executed and returned {result}");
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception thrown: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                        Console.WriteLine("Closing sql connection");
                    }
                }
            }

            return -1;
        }

        public DataTable ExecuteQueryStoredProcedure(string storedProcedureName, IDbDataParameter[]? parameters = null)
        {
            var dtResult = new DataTable();
            using (var connection = new MySqlConnection(ConnectionString))
            {

                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (parameters != null && parameters.Any())
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        Console.WriteLine("Openning sql connection");
                        using (var reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Filling Data table..");
                            dtResult.Load(reader);
                            Console.WriteLine($"{storedProcedureName} executed and returned {dtResult.Rows.Count} rows");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception thrown: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                        Console.WriteLine("Closing sql connection");
                    }
                }
            }

            return dtResult;
        }
    }
}