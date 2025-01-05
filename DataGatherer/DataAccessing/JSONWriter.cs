public static class JSONWriter
{
    public static void SaveAllTablesAsJson(string fileName, List<List<Dictionary<string, string>>> tablesFromHtml)
    {
        var allTables = tablesFromHtml;
        var json = System.Text.Json.JsonSerializer.Serialize(allTables, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(fileName, json);
    }
}