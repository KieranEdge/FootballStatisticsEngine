using DataGatherer.DataAccessing;
using DataGatherer.URLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataGatherer.DataStorage
{
    public class MatchReportBatchProcessor
    {
        private int _startYear;
        private int _endYear;
        private string _teamName;

        public MatchReportBatchProcessor(int startYear, int endYear, string teamName) 
        {
            _startYear = startYear;
            _endYear = endYear;
            _teamName = teamName;
        }

        public async void Process()
        {
            for (int year = _startYear; year <= _endYear; year++)
            {
                // Creating the website url for accessing the web page
                string websiteUrl = UrlFormatting.UrlCreator(year, year + 1);
                Console.WriteLine($"Accessing data from: {websiteUrl}");
                string competition = "English Football League";


                // Getting the html content from the url
                string htmlContent = await DataAccessing.DataAccessing.HtmlAccesser(websiteUrl);

                // Extracting all of the table data from the raw html
                List<Dictionary<string, string>> extractedReports = await MatchReportExtracter.ExtractAllMatchReports(htmlContent);

                if (extractedReports.Count > 0)
                {
                    competition = extractedReports[0]["Comp"];
                }

                //// Saving all data to a database
                MatchReportTableCreator.CreateMatchReportTable(year, year + 1);
                MatchReportDatabaseStorage.MatchReportDatabaseCreator(_teamName, year, year + 1, extractedReports, competition);
            }
        }

    }
}
