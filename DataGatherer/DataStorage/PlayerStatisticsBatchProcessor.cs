using DataGatherer.URLTools;
using DataGatherer.DataAccessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGatherer.HelperMethods;

namespace DataGatherer.DataStorage
{
    public class PlayerStatisticsBatchProcessor
    {
        private string _teamName;
        private int _startYear;
        private int _endYear;

        public PlayerStatisticsBatchProcessor(string teamName, int startYear, int endYear)
        {
            _teamName = teamName;
            _startYear = startYear;
            _endYear = endYear;
        }

        public void Process()
        {
            Console.WriteLine($"Gathering Player Reports from season {_startYear} to {_endYear}");
            for (int i = _startYear; i <= _endYear; i++)
            {
                // Constructing the url
                string websiteUrl = UrlFormatting.UrlCreator(i, i + 1);
                Console.WriteLine($"Accessing data from: {websiteUrl}");

                // Getting the html content from the url
                string htmlContent = DataAccessing.DataAccessing.HtmlAccesser(websiteUrl).Result;
               
                // Creating the player reports table
                string tableName = PlayerReportTableNameCreator.TableNameCreator(i, i + 1, _teamName);
                Console.WriteLine(tableName);
                PlayerReportTableCreator newTable = new PlayerReportTableCreator(tableName);
                newTable.CreateDatabaseTable();

                // Parsing the results and writing to the table
                PlayerStatisticsDatabaseStorage storage = new PlayerStatisticsDatabaseStorage();
                List<PlayerStats> playerStats = storage.ParseHtmlTable(htmlContent);
                storage.SaveToDatabase(playerStats, tableName);
            }
        }
    }
}
