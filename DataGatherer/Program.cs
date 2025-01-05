using System;
using System.Net.Http;
using HtmlAgilityPack;

using DataGatherer.URLTools;
using DataGatherer.DataAccessing;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");

// Specifying the url
string dummyUrl = UrlFormatting.UrlCreator(2022, 2023);
Console.WriteLine($"Accessing data from: {dummyUrl}");

// Getting the html content from the url
string htmlContent = await DataAccessing.HtmlAccesser(dummyUrl);

// Extracting all of the table data from the raw html
List<List<Dictionary<string, string>>> extractedTables = await TableExtracter.ExtractAllTables(htmlContent);

// Saving all of the Table Data to JSON
JSONWriter.SaveAllTablesAsJson("ExtractedTables.JSON", extractedTables);

Console.WriteLine("The program is complete");
Console.ReadKey();

