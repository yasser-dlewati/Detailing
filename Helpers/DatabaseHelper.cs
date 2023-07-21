using System;
using System.Data;
using Detailing.Interfaces;
using MySql.Data.MySqlClient;

namespace Detailing.Helpers
{
    public class DatabaseHelper<T> : IDatabaseHelper<T>
    {
        private static string connectionString = "Server=localhost;User ID=root;Password=password;Database=detailing";

        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, MySqlParameter[] parameters)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    try
                    {
                        Console.WriteLine("Openning sql connection");
                        connection.Open();
                        command.ExecuteNonQuery();

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
        }
        
        public DataTable ExecuteQueryStoredProcedure(string storedProcedureName)
        {
            var dtResult = new DataTable();
            using (var connection = new MySqlConnection(connectionString))
            {

                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Openning sql connection");
                        using (var reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Filling Data..");
                            dtResult.Load(reader);
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

        public T ExecuteScalarQueryStoredProcedure(string storedProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}