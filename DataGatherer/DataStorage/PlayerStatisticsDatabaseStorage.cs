using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataGatherer.DataStorage
{
    public class PlayerStatisticsDatabaseStorage
    {
        private string _connectionString = "Server = localhost; Database = PlayerDatabase; Trusted_Connection = True";
        public List<PlayerStats> ParseHtmlTable(string html)
        {
            var players = new List<PlayerStats>();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var table = doc.DocumentNode.SelectSingleNode("//table[@id='stats_standard_10']");
            var rows = table.SelectNodes(".//tbody/tr");

            foreach (var row in rows)
            {
                var cells = row.SelectNodes(".//td");

                // Find the player name in the <th> tag (this is the first cell, before the <td> cells)
                var playerNameNode = row.SelectSingleNode(".//th/a");

                // Extract player name from the <a> tag
                var playerName = playerNameNode != null ? playerNameNode.InnerText.Trim() : "Unknown Player";


                // Continue extracting other values (cells)
                if (cells != null && cells.Count >= 17)  // Ensure there are enough cells in the row
                {
                    players.Add(new PlayerStats
                    {
                        Player = playerName,  // Capture player name directly from <a> tag
                        Nation = cells[0].InnerText.Trim(),
                        Position = cells[1].InnerText.Trim(),
                        Age = int.TryParse(cells[2].InnerText.Trim(), out int age) ? age : 0,
                        MatchesPlayed = int.TryParse(cells[4].InnerText.Trim(), out int mp) ? mp : 0,
                        Goals = int.TryParse(cells[9].InnerText.Trim(), out int goals) ? goals : 0,
                        Assists = int.TryParse(cells[10].InnerText.Trim(), out int assists) ? assists : 0,
                        YellowCards = int.TryParse(cells[15].InnerText.Trim(), out int yellows) ? yellows : 0,
                        RedCards = int.TryParse(cells[16].InnerText.Trim(), out int reds) ? reds : 0
                    });
                }
            }
            return players;
        }

        public void SaveToDatabase(List<PlayerStats> players, string tableName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var player in players)
                {
                    var command = new SqlCommand(
                        $"INSERT INTO [{tableName}] (Player, Nation, Position, Age, MatchesPlayed, Goals, Assists, YellowCards, RedCards) " +
                        "VALUES (@Player, @Nation, @Position, @Age, @MatchesPlayed, @Goals, @Assists, @YellowCards, @RedCards)", connection);

                    command.Parameters.AddWithValue("@Player", player.Player);
                    command.Parameters.AddWithValue("@Nation", player.Nation);
                    command.Parameters.AddWithValue("@Position", player.Position);
                    command.Parameters.AddWithValue("@Age", player.Age);
                    command.Parameters.AddWithValue("@MatchesPlayed", player.MatchesPlayed);
                    command.Parameters.AddWithValue("@Goals", player.Goals);
                    command.Parameters.AddWithValue("@Assists", player.Assists);
                    command.Parameters.AddWithValue("@YellowCards", player.YellowCards);
                    command.Parameters.AddWithValue("@RedCards", player.RedCards);

                    command.ExecuteNonQuery();
                }
            }
        }

    }

    public class PlayerReportTableCreator
    {
        private string _connectionString = "Server = localhost; Database = PlayerDatabase; Trusted_Connection = True";
        private string _tableName;

        public PlayerReportTableCreator(string tableName)
        { 
            _tableName = tableName;
        }

        public void CreateDatabaseTable()
        {
            string createTableQuery = $@"
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{_tableName}' AND xtype='U')
        CREATE TABLE [{_tableName}] (
            Id INT PRIMARY KEY IDENTITY(1,1),
            Player NVARCHAR(100) NOT NULL,
            Nation NVARCHAR(50),
            Position NVARCHAR(10),
            Age INT,
            MatchesPlayed INT,
            Goals INT,
            Assists INT,
            YellowCards INT,
            RedCards INT
        );";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public class PlayerStats
    {
        public string Player { get; set; }
        public string Nation { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int MatchesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
}
