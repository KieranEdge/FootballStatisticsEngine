// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json.Serialization;
using FootballStatisticsEngine.Connections;
using FootballStatisticsEngine.Contexts;
using FootballStatisticsEngine.Models;

Console.WriteLine("Welcome to the football statistics engine application!");
string connectionString = "";

try
{
    // Retrieve the resolved connection string
    connectionString = ConnectionStringResolver.GetConnectionString("Football_DB_connectionString");
    Console.WriteLine($"Resolved Connection String: {connectionString}");

    // Use the connection string (e.g., for database operations)
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

// Initialising the database connection
using (var context = new MatchReportDbContext(connectionString))
{
    var reports = context.GetReportsFromYear("MatchReports_2020_2021");

    foreach (var report in reports)
    { Console.WriteLine(report.Opponent); }
}