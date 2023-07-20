using System;
using System.Data;
using Detailing.Helpers;
using MySql.Data.MySqlClient;

namespace Detailing.Helpers
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;User ID=root;Password=password;Database=detailing";
        public static DataTable ExecuteReadStoredProcedure(string storedProcedureName)
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
                        Console.WriteLine($"Exception thrown: {ex.Message}" );
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

        public static bool ExecuteWriteStoredProcedure(string storedProcedureName)
        {
            var result = 0;
            using (var connection = new MySqlConnection(connectionString))
            {

                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Openning sql connection");
                        result = command.ExecuteNonQuery();
                        return result == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error catched: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                        Console.WriteLine("Closing sql connection");
                    }
                }
            }

            return false;
        }
    }
}