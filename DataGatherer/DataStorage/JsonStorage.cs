using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataGatherer.DataStorage
{
    public static class JsonStorage
    {
        public static void SaveToJson(List<Dictionary<string, string>> rows, string filePath)
        {
            var json = JsonSerializer.Serialize(rows, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
