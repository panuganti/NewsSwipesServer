using System;
using System.Linq;

namespace NewsSwipesLibrary
{
    public class Utils
    {
        public ArticleData GetArticleData(string url)
        {
            var uri = new Uri(url);
            switch(uri.Host)
            {
                case "http://timesofindia.indiatimes.com/": return TimesOfIndiaExtractor(url);
                default: return DefaultArticleExtractor(url);
            }
        }

        public ArticleData TimesOfIndiaExtractor(string url)
        {
            throw new NotImplementedException();
        }

        public ArticleData DefaultArticleExtractor(string url)
        {
            try
            {
                var html = new HtmlDocument();
                html.Load(url);
                var titleNode = html.DocumentNode.Descendants("title").First();
                var images = html.DocumentNode.Descendants("img").Where(img => img.Attributes.Contains("src")).Select(img => img.Attributes["src"].Value);

                return new ArticleData
                {
                    Title = titleNode.InnerText,
                    ArticleDate = DateTime.Now,
                    ImageUrls = images.ToArray()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
