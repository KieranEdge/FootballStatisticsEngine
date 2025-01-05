using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGatherer.DataAccessing
{
    public class TableExtracter
    {
        public static async Task<List<List<Dictionary<string, string>>>> ExtractAllTables(string html)
        {
            // Load the HTML document
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Find all tables in the document
            var tables = htmlDoc.DocumentNode.SelectNodes("//table");
            if (tables == null || tables.Count == 0)
            {
                throw new Exception("No tables found in the HTML.");
            }

            var allTablesData = new List<List<Dictionary<string, string>>>();

            foreach (var table in tables)
            {
                // Extract headers
                var headers = table.SelectNodes(".//thead//th")
                    ?.Select(th => th.InnerText.Trim())
                    .ToList();

                if (headers == null || headers.Count == 0)
                {
                    // If no <thead>, attempt to use the first row (<tr>) as headers
                    var headerRow = table.SelectSingleNode(".//tr");
                    if (headerRow != null)
                    {
                        headers = headerRow.SelectNodes(".//th | .//td")
                            ?.Select(cell => cell.InnerText.Trim())
                            .ToList();
                    }
                }

                // Extract rows
                var rows = table.SelectNodes(".//tr");
                var tableData = new List<Dictionary<string, string>>();

                foreach (var row in rows)
                {
                    var cells = row.SelectNodes(".//td");

                    // Skip rows without table data cells
                    if (cells == null) continue;

                    var rowDict = new Dictionary<string, string>();

                    // Handle header mismatch: Match only up to the smaller of the two counts
                    for (int i = 0; i < Math.Max(headers?.Count ?? 0, cells.Count); i++)
                    {
                        string header = headers != null && i < headers.Count ? headers[i] : $"Column{i + 1}";
                        string cellValue = i < cells.Count ? cells[i].InnerText.Trim() : string.Empty;
                        rowDict[header] = cellValue;
                    }

                    tableData.Add(rowDict);
                }

                allTablesData.Add(tableData);
            }

            return allTablesData;
        }
    }
}