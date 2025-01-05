using System;
using System.Net.Http;
using HtmlAgilityPack;

using DataGatherer.URLTools;
using DataGatherer.DataAccessing;
using DataGatherer.DataStorage;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
int startYear = 2022;
int endYear = 2023;
string teamName = "Sheffield Wednesday";


// Specifying the url
string dummyUrl = UrlFormatting.UrlCreator(startYear, endYear);
Console.WriteLine($"Accessing data from: {dummyUrl}");

// Getting the html content from the url
string htmlContent = await DataAccessing.HtmlAccesser(dummyUrl);

// Extracting all of the table data from the raw html
List<Dictionary<string, string>> extractedReports = await MatchReportExtracter.ExtractAllMatchReports(htmlContent);
string competition = extractedReports[0]["Comp"];

//// Saving all of the Table Data to JSON
//JsonStorage.SaveToJson(extractedReports, "draftAttempt.json");


//// Saving all data to a database
MatchReportTableCreator.CreateMatchReportTable(startYear, endYear);
DatabaseStorage.MatchReportDatabaseCreator(teamName, startYear, endYear, extractedReports, competition);

Console.WriteLine("The program is complete");
Console.ReadKey();

