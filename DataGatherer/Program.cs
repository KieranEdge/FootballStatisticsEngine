using System;
using System.Net.Http;
using HtmlAgilityPack;

using DataGatherer.URLTools;
using DataGatherer.DataAccessing;
using DataGatherer.DataStorage;
using DataGatherer.DataManipulation;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
int startYear = 2022;
int endYear = 2025;
string teamName = "Sheffield Wednesday";

Console.WriteLine("Beginning...");

PlayerStatisticsBatchProcessor reportBatchProcessor = new PlayerStatisticsBatchProcessor(teamName, startYear, endYear);

Console.WriteLine("Batch Processor Created");
reportBatchProcessor.Process();
