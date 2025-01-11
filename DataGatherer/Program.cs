using System;
using System.Net.Http;
using HtmlAgilityPack;

using DataGatherer.URLTools;
using DataGatherer.DataAccessing;
using DataGatherer.DataStorage;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
int startYear = 2000;
int endYear = 2021;
string teamName = "Sheffield Wednesday";

// Initialising the Batch Processor
BatchProcessor batchProcessor = new BatchProcessor(startYear, endYear, teamName);
batchProcessor.Process();

Console.WriteLine("The program is complete");
Console.ReadKey();
