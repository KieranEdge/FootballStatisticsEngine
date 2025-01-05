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
                var headers = table.SelectNodes(".//thead//th")
                    ?.Select(th => th.InnerText.Trim())
                    .ToList();

                var rows = table.SelectNodes(".//tr");
                var tableData = new List<Dictionary<string, string>>();

                foreach (var row in rows)
                {
                    var cells = row.SelectNodes(".//td");

                    // Skip rows without table data cells
                    if (cells == null) continue;

                    if (headers != null && headers.Count > 0)
                    {
                        var rowDict = new Dictionary<string, string>();
                        for (int i = 0; i < headers.Count; i++)
                        {
                            var header = headers[i];
                            var cellValue = i < cells.Count ? cells[i].InnerText.Trim() : string.Empty;
                            rowDict[header] = cellValue;
                        }
                        tableData.Add(rowDict);
                    }
                    else
                    {
                        var rowDict = new Dictionary<string, string>();
                        for (int i = 0; i < cells.Count; i++)
                        {
                            rowDict[$"Column{i + 1}"] = cells[i].InnerText.Trim();
                        }
                        tableData.Add(rowDict);
                    }
                }

                allTablesData.Add(tableData);
            }

            return allTablesData;
        }
    }
}