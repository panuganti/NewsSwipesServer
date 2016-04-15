using System;
using System.Linq;
using HtmlAgilityPack;
using DataContracts.Client;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

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

        private string GetHtmlFromUrl(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
                return data;
            }
            throw new Exception("Fetching Url Failed");
        }

        private string GetAbsoluteUri(string url, string homeUrl)
        {
            Uri result;
            var isAbsolute = Uri.TryCreate(url, UriKind.Absolute, out result);

            if (isAbsolute) { return url; }
            else {
                return new Uri(new Uri(homeUrl), url).AbsoluteUri;
            }
        }

        private string GetDescription(HtmlNode documentNode)
        {
            HtmlNode mdnode = documentNode.SelectSingleNode("//meta[@name='twitter:description']");
            if (mdnode != null)
            {
                HtmlAttribute desc;
                desc = mdnode.Attributes["content"];
                return desc.Value;
            }

            mdnode = documentNode.SelectSingleNode("//meta[@name='description']");
            if (mdnode != null)
            {
                HtmlAttribute desc;
                desc = mdnode.Attributes["content"];
                return desc.Value;
            }
            return string.Empty;
        }

        public PostPreview DefaultArticleExtractor(string url)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                string html = GetHtmlFromUrl(url);
                htmlDoc.LoadHtml(html);
                var titleNode = htmlDoc.DocumentNode.Descendants("title").First();

                var node = htmlDoc.DocumentNode;
                string fullDescription = GetDescription(node); 

                var images = htmlDoc.DocumentNode.Descendants("img")
                    .Where(img => img.Attributes.Contains("src"))
                    .Select(img => GetAbsoluteUri(img.Attributes["src"].Value, url)).Distinct();
                

                var postPreview = new PostPreview
                {
                    Heading = titleNode.InnerText,
                    Date = DateTime.Now.ToUniversalTime().ToString(),
                    CardStyle = "Horizontal", // TODO: Enum
                    Snippet = fullDescription, // TODO:
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

        public async Task<string> GetLandingPageUrl(string url)
        {
            try
            {
                // Create a New HttpClient object.
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseUri = response.RequestMessage.RequestUri.ToString();
                return responseUri;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public string[] ExtractImages(string url)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                string html = GetHtmlFromUrl(url);
                htmlDoc.LoadHtml(html);
                var images = htmlDoc.DocumentNode.Descendants("img")
                    .Where(img => img.Attributes.Contains("src"))
                    .Select(img => new Uri(new Uri(url), img.Attributes["src"].Value).AbsoluteUri).Distinct();

                return images.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
