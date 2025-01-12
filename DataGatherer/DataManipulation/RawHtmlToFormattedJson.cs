using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace DataGatherer.DataManipulation
{
    public class RawHtmlToFormattedJson
    {
        private string _htmlContent;
        public RawHtmlToFormattedJson(string htmlContent)
        {
            _htmlContent = htmlContent;
        }

        public void HtmlToJsonWriter()
        {
            // Load the HTML into HtmlDocument
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(_htmlContent);

            // Convert the HTML into a Dictionary or Object Model (you can adjust this as needed)
            var htmlJson = ParseHtmlToJson(htmlDoc.DocumentNode);

            // Serialize the object to JSON
            string json = JsonConvert.SerializeObject(htmlJson, Formatting.Indented);

            // Write the JSON to a file
            File.WriteAllText("output.json", json);
            Console.WriteLine("HTML converted to JSON successfully!");
        }

        // Function to parse HTML into a Dictionary
        static Dictionary<string, object> ParseHtmlToJson(HtmlNode rootNode)
        {
            var result = new Dictionary<string, object>();

            // Process the root node (html or body or other tag)
            if (rootNode != null)
            {
                result["NodeName"] = rootNode.Name;
                result["Attributes"] = rootNode.Attributes.Count > 0 ? GetAttributes(rootNode) : null;
                result["InnerHtml"] = rootNode.InnerHtml;

                // Process child nodes (if any)
                var childNodes = new List<Dictionary<string, object>>();
                foreach (var child in rootNode.ChildNodes)
                {
                    childNodes.Add(ParseHtmlToJson(child));
                }

                result["Children"] = childNodes;
            }

            return result;
        }

        // Helper function to get the attributes of a node
        static Dictionary<string, string> GetAttributes(HtmlNode node)
        {
            var attributes = new Dictionary<string, string>();
            foreach (var attribute in node.Attributes)
            {
                attributes[attribute.Name] = attribute.Value;
            }
            return attributes;
        }

    }
}

