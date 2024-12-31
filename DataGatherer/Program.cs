Console.WriteLine("The purpose of this program is to gather data from a remote website and save it to a JSON object");
string dummyUrl = UrlFormatter.UrlCreator(2022, 2023);
Console.WriteLine(dummyUrl);
Console.ReadKey();


public static class UrlFormatter
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