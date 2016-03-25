using System;
using System.Linq;
using HtmlAgilityPack;
using DataContracts.Client;

namespace NewsSwipesLibrary
{
    public class Utils
    {
        public PostPreview GetArticleData(string url)
        {
            var uri = new Uri(url);
            switch(uri.Host)
            {
                case "http://timesofindia.indiatimes.com/": return TimesOfIndiaExtractor(url);
                default: return DefaultArticleExtractor(url);
            }
        }

        public PostPreview TimesOfIndiaExtractor(string url)
        {
            return DefaultArticleExtractor(url); // TODO:
        }

        public PostPreview DefaultArticleExtractor(string url)
        {
            try
            {
                var html = new HtmlDocument();
                html.Load(url);
                var titleNode = html.DocumentNode.Descendants("title").First();
                var images = html.DocumentNode.Descendants("img")
                    .Where(img => img.Attributes.Contains("src")).Select(img => img.Attributes["src"].Value);

                var postPreview = new PostPreview
                {
                    Heading = titleNode.InnerText,
                    Date = DateTime.Now.ToUniversalTime().ToString(),
                    CardStyle = "Horizontal", // TODO: Enum
                    Snippet = "", // TODO:
                    OriginalLink = url,
                    Images = images.ToArray(), // TODO: Filter out bad res. images
                    ImagesFromDb = new DbImage[] { } // TODO:
                };
                return postPreview;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
