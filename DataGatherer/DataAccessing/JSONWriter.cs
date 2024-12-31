using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataGatherer.DataAccessing
{
    public static class JSONWriter
    {
        public static void HtmlToJson(string html, string fileName)
        {
            // Serialize the object to a JSON string
            string jsonString = JsonSerializer.Serialize(html, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }
    }
}
