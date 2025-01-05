using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGatherer.DataAccessing
{
    public class MatchReportExtracter
    {
        public static async Task<List<Dictionary<string, string>>> ExtractAllMatchReports(string html) 
        {
            // Using HtmlAgilityPack to unpack the html data provided
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var rows = new List<Dictionary<string, string>>();

            // Find the table by ID
            var table = document.DocumentNode.SelectSingleNode("//table[@id='matchlogs_for']");
            if (table == null)
            {
                Console.WriteLine("Table not found!");
                return rows;
            }

            // Extract headers
            var headers = new List<string>();
            foreach (var header in table.SelectNodes(".//thead/tr/th"))
            {
                headers.Add(header.InnerText.Trim());
            }

            // Extract rows
            foreach (var row in table.SelectNodes(".//tbody/tr"))
            {
                var rowData = new Dictionary<string, string>();
                var cells = row.SelectNodes("./th|./td"); // Select <th> or <td> cells
                for (int i = 0; i < cells.Count; i++)
                {
                    string header = headers[i];
                    string value = cells[i].InnerText.Trim();
                    rowData[header] = value;
                }
                rows.Add(rowData);
            }

            return rows;
        }
    }
}
