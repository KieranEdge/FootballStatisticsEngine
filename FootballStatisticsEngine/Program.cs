// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;
using FootballStatisticsEngine.Connections;

Console.WriteLine("Welcome to the football statistics engine application!");

try
{
    // Retrieve the resolved connection string
    string connectionString = ConnectionStringResolver.GetConnectionString("Football_DB_connectionString");
    Console.WriteLine($"Resolved Connection String: {connectionString}");

    // Use the connection string (e.g., for database operations)
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}