using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace DataGatherer.DataManipulation
{
    public static class RawHtmlOutputter
    {
        public static void WriteHtmlToFile(string htmlContent, string filePath)
        {
            // Write the HTML content to the specified file
            File.WriteAllText(filePath, htmlContent);

            Console.WriteLine($"HTML content has been written to {filePath}");
        }
    }
}
