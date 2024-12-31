using System;
using System.Net.Http;
using HtmlAgilityPack;

Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
string dummyUrl = UrlFormatting.UrlCreator(2022, 2023);
Console.WriteLine($"Accessing data from: {dummyUrl}");
string htmlContent = DataAccessing.HtmlAccesser(dummyUrl).Result;
Console.WriteLine(htmlContent);
Console.ReadKey();


public static class UrlFormatting
{
    /// <summary>
    /// Static class for constructing the url for accessing the data
    /// </summary>

    public static string UrlCreator(int startYear, int endYear)
    { 
    
        string urlModifier =  $"https://fbref.com/en/squads/bba7d733/{startYear.ToString()}-{endYear.ToString()}/Sheffield-Wednesday-Stats";
        
        return urlModifier;
    }

}

public static class DataAccessing
{
    /// <summary>
    /// Class for returning entire HTML elements from a webpage
    /// </summary>
    /// 
    public async static Task<string> HtmlAccesser(string url)
    {
        try
        {
            // Create an HttpClient instance
            using HttpClient client = new HttpClient();

            // Send a GET request to the URL and get the response
            string htmlContent = await client.GetStringAsync(url);

            return htmlContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return "HTML could not be accessed";
        }
    }
}