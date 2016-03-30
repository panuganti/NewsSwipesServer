using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DataContracts.Server;
using DataContracts.Client;
using Search;
using System.Threading.Tasks;
using DataContracts.Search;
using Microsoft.Azure.Search.Models;

namespace NewsSwipesLibrary
{
    public class Feeds
    {
        private Dictionary<string, string> _feeds = new Dictionary<string, string>() {
            {"toi", "http://timesofindia.indiatimes.com/rssfeedsdefault.cms" },
            {"ndtv", "http://feeds.feedburner.com/NdtvNews-TopStories?format=xml" }
        };

        private Utils _utils;
        FeedsIndex _feedsIndex;

        public Feeds(Utils utils, FeedsIndex feedsIndex)
        {
            _utils = utils;
            _feedsIndex = feedsIndex;
        }

        public IEnumerable<Feed> LoadNdtvRssFeeds()
        {
            string feedUrl = _feeds["ndtv"];
            XDocument xDoc = XDocument.Load(feedUrl);
            var feeds = xDoc.Descendants("item")
                .Select(
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = t.Element("link").Value,
                            PublishedDate = DateTime.Parse(t.Element("pubDate").Value),
                            Description = HtmlRemoval.StripTagsRegex(t.Element("description").Value)
                        }).ToList();
            return feeds;
        }

        public List<Feed> LoadTOIRssFeeds()
        {
            string feedUrl = _feeds["toi"];
            XDocument xDoc = XDocument.Load(feedUrl);
            var feeds = xDoc.Descendants("item")
                .Select(
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = t.Element("link").Value,
                            PublishedDate = DateTime.Parse(t.Element("pubDate").Value),
                            Description = t.Element("description").Value
                        });
            return feeds.ToList();
        }

        public async Task<PostPreview> LoadNextFeed(string stream)
        {
            var feeds = LoadTOIRssFeeds();
            foreach(var feed in feeds)
            {
                try {
                    var sp = new SearchParameters()
                    {
                        Filter = String.Format("landingpageurl eq '{0}'", feed.Link.ToLower())
                    };
                    var response = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*", sp);
                    if (response.Results.Count() > 0) { continue; } // Instead get totalcount
                    return ToPostPreview(feed);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            return ToPostPreview(feeds.First());
        }

        public PostPreview ToPostPreview(Feed feed)
        {
            var postPreview = new PostPreview()
            {
                Date = feed.PublishedDate.ToString(),
                CardStyle = "Horizontal",
                Heading = feed.Title,
                Snippet = feed.Description,
                OriginalLink = feed.Link,
                Images = _utils.ExtractImages(feed.Link),
                ImagesFromDb = new List<DbImage>().ToArray()
            };
            return postPreview;
        }
    }
}
