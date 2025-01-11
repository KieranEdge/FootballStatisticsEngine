using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataGatherer.DataStorage
{
    public static class DatabaseStorage
    {
        public static void MatchReportDatabaseCreator(string teamName, int startYear, int endYear, List<Dictionary<string, string>> matches, string competition)
        {
            string connectionString = "Server = localhost; Database = FootballDatabase; Trusted_Connection = True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 1. Insert Metadata into Main Table
                string insertMainTableQuery = @"
                INSERT INTO MatchReports (TeamName, Competition, StartYear, EndYear)
                VALUES (@TeamName, @Competition, @StartYear, @EndYear);
                SELECT SCOPE_IDENTITY();";

                var mainTableCommand = new SqlCommand(insertMainTableQuery, connection);
                mainTableCommand.Parameters.AddWithValue("@TeamName", teamName);
                mainTableCommand.Parameters.AddWithValue("@Competition", competition);
                mainTableCommand.Parameters.AddWithValue("@StartYear", startYear);
                mainTableCommand.Parameters.AddWithValue("@EndYear", endYear);


                // Get the newly inserted ID
                int matchReportId = Convert.ToInt32(mainTableCommand.ExecuteScalar());

                // 2. Insert Match Data into Yearly Sub-Table
                string insertSubTableQuery = $@"
                INSERT INTO MatchReports_{startYear}_{endYear} 
                (MatchDate, StartTime, Competition, Round, Day, Venue, Result, GoalsFor, GoalsAgainst, Opponent, Possession, Attendance, Captain, Formation, OppFormation, Referee, MatchReport, Notes)
                VALUES 
                (@MatchDate, @StartTime, @Competition, @Round, @Day, @Venue, @Result, @GoalsFor, @GoalsAgainst, @Opponent, @Possession, @Attendance, @Captain, @Formation, @OppFormation, @Referee, @MatchReport, @Notes);
                ";

                foreach (var match in matches)
                {
                    var subTableCommand = new SqlCommand(insertSubTableQuery, connection);

                    subTableCommand.Parameters.AddWithValue("@MatchDate", GetDbValue(match["Date"]));
                    subTableCommand.Parameters.AddWithValue("@StartTime", GetDbValue(match["Time"]));
                    subTableCommand.Parameters.AddWithValue("@Competition", GetDbValue(match["Comp"]));
                    subTableCommand.Parameters.AddWithValue("@Round", GetDbValue(match["Round"]));
                    subTableCommand.Parameters.AddWithValue("@Day", GetDbValue(match["Day"]));
                    subTableCommand.Parameters.AddWithValue("@Venue", GetDbValue(match["Venue"]));
                    subTableCommand.Parameters.AddWithValue("@Result", GetDbValue(match["Result"]));
                    subTableCommand.Parameters.AddWithValue("@GoalsFor", GetDbValue(match["GF"]?.Split(" ")[0]));
                    subTableCommand.Parameters.AddWithValue("@GoalsAgainst", GetDbValue(match["GA"]?.Split(" ")[0]));
                    subTableCommand.Parameters.AddWithValue("@Opponent", GetDbValue(match["Opponent"]));
                    subTableCommand.Parameters.AddWithValue("@Possession", GetDbValue(match["Poss"]));
                    subTableCommand.Parameters.AddWithValue("@Attendance", ParseAttendance(match["Attendance"]));
                    subTableCommand.Parameters.AddWithValue("@Captain", GetDbValue(match["Captain"]));
                    subTableCommand.Parameters.AddWithValue("@Formation", GetDbValue(match["Formation"]));
                    subTableCommand.Parameters.AddWithValue("@OppFormation", GetDbValue(match["Opp Formation"]));
                    subTableCommand.Parameters.AddWithValue("@Referee", GetDbValue(match["Referee"]));
                    subTableCommand.Parameters.AddWithValue("@MatchReport", GetDbValue(match["Match Report"]));
                    subTableCommand.Parameters.AddWithValue("@Notes", GetDbValue(match["Notes"]));

                    subTableCommand.ExecuteNonQuery();
                }

                Console.WriteLine($"Data saved successfully for {teamName} in {competition} for the {startYear}/{endYear} Season");
            }
        }

        public static object GetDbValue(string value)
        {
            return string.IsNullOrEmpty(value) ? DBNull.Value : value;
        }

        public static object ParseAttendance(string attendance)
        {
            if (string.IsNullOrEmpty(attendance))
            {
                return DBNull.Value;
            }

            if (int.TryParse(attendance.Replace(",", ""), out int result))
            {
                return result;
            }

            return DBNull.Value;
        }


    }

    public static class MatchReportTableCreator
    {
        public static void CreateMatchReportTable(int startYear, int endYear)
        {
            //Creating the table name
            string tableName = $"MatchReports_{startYear.ToString()}_{endYear.ToString()}";

            // Database connection string
            string connectionString = "Server = localhost; Database = FootballDatabase; Trusted_Connection = True";

            // SQL query to check if the table exists
            string checkTableQuery = $@"
            SELECT COUNT(*) 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}'";

            // SQL query to create the table if it doesn't exist
            string createTableQuery = $@"
            CREATE TABLE {tableName} (
                MatchID INT IDENTITY(1,1) PRIMARY KEY,
                MatchDate DATE NOT NULL,
                StartTime TIME NOT NULL,
                Competition NVARCHAR(100) NOT NULL,
                Round NVARCHAR(50),
                Day NVARCHAR(10),
                Venue NVARCHAR(20),
                Result NVARCHAR(5),
                GoalsFor INT,
                GoalsAgainst INT,
                Opponent NVARCHAR(100),
                Possession INT,
                Attendance INT,
                Captain NVARCHAR(100),
                Formation NVARCHAR(20),
                OppFormation NVARCHAR(20),
                Referee NVARCHAR(100),
                MatchReport NVARCHAR(MAX),
                Notes NVARCHAR(MAX)
            );";

            try
            {
                // Connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the table exists
                    using (SqlCommand checkTableCmd = new SqlCommand(checkTableQuery, connection))
                    {
                        int tableExists = (int)checkTableCmd.ExecuteScalar();
                        if (tableExists == 0) // If the table does not exist
                        {
                            Console.WriteLine($"Table '{tableName}' does not exist. Creating it now...");

                            // Create the table
                            using (SqlCommand createTableCmd = new SqlCommand(createTableQuery, connection))
                            {
                                createTableCmd.ExecuteNonQuery();
                                Console.WriteLine($"Table '{tableName}' has been created successfully.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Table '{tableName}' already exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}


