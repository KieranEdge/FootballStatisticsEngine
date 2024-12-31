using System;
using System.Net.Http;
using HtmlAgilityPack;

using DataGatherer.URLTools;
using DataGatherer.DataAccessing;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
string dummyUrl = UrlFormatting.UrlCreator(2022, 2023);
Console.WriteLine($"Accessing data from: {dummyUrl}");
string htmlContent = DataAccessing.HtmlAccesser(dummyUrl).Result;
JSONWriter.HtmlToJson(htmlContent, "C:\\Users\\Kieran Edge\\source\\repos\\FootballStatisticsEngine\\DataGatherer\\OutputData\\dummyContent.json");
Console.WriteLine("The program is complete");
Console.ReadKey();

