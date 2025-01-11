namespace DataGatherer.DataAccessing
{
    public class DataAccessing
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
}