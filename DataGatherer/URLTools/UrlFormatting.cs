namespace DataGatherer.URLTools
{
    public static class UrlFormatting
    {
        /// <summary>
        /// Static class for constructing the url for accessing the data
        /// </summary>

        public static string UrlCreator(int startYear, int endYear)
        {

            string urlModifier = $"https://fbref.com/en/squads/bba7d733/{startYear.ToString()}-{endYear.ToString()}/Sheffield-Wednesday-Stats";

            return urlModifier;
        }

    }
}

