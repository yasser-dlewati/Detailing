using System.Data;
using Detailing.Extensions;
using Detailing.Interfaces;
using MySql.Data.MySqlClient;

namespace Detailing.Services
{
    public class MySqlDatabaseService : IDatabaseService
    {
        public string ConnectionString { get; set; }
        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[] parameters)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                using (var command = new MySqlCommand(storedProcedureName, connection))
                {
                    var mysqlParameters = parameters.ToMySqlParameters().ToArray();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(mysqlParameters);
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
                        Console.WriteLine($"Exception thrown while executing {storedProcedureName}: {ex.Message}");
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

        public async Task<DataTable> ExecuteQueryStoredProcedureAsync(string storedProcedureName, IDbDataParameter[]? parameters = null)
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
                            var mysqlParameters = parameters.ToMySqlParameters().ToArray();
                            command.Parameters.AddRange(mysqlParameters);
                        }

                        connection.Open();
                        Console.WriteLine("Openning sql connection");
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            Console.WriteLine("Filling Data table..");
                            do 
                            {
                                if(reader.FieldCount >1)
                                {
                                    dtResult.Load(reader);
                                    break;
                                }
                            }
                            while(reader.NextResult());
                            
                            Console.WriteLine($"{storedProcedureName} executed and returned {dtResult.Rows.Count} rows");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception thrown while executing {storedProcedureName}: {ex.Message}\n {ex.StackTrace}");
                    }
                    finally
                    {
                        if(connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        Console.WriteLine("Closing sql connection");
                    }
                }
            }

            return dtResult;
        }
    }
}