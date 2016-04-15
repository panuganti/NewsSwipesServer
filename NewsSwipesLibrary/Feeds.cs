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
using System.Text.RegularExpressions;

namespace NewsSwipesLibrary
{
    public class Feeds
    {
        private static int engCount = 0;
        private static int hindiCount = 0;
        private static int marathiCount = 0;
        private static int teluguCount = 0;

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        private Dictionary<string, string> _englishfeeds = new Dictionary<string, string>() {
            {"toi", "http://timesofindia.indiatimes.com/rssfeedsdefault.cms" },
            {"ndtv", "http://feeds.feedburner.com/NdtvNews-TopStories?format=xml" }
        };

        private Dictionary<string, string> _hindifeeds = new Dictionary<string, string>() {
            {"ndtv_hindi", "http://feeds.feedburner.com/ndtvkhabar?format=xml" },
            {"jagran", "http://rss.jagran.com/rss/news/national.xml" },
        };

        private Dictionary<string, string> _marathifeeds = new Dictionary<string, string>() {
            {"loksatta", "http://www.loksatta.com/mumbai/feed/" }
        };

        private Dictionary<string, string> _telugufeeds = new Dictionary<string, string>() {
            {"oneindia_telugunews", "http://telugu.oneindia.com/rss/telugu-news-fb.xml" },
            {"oneindia_telugufeature", "http://telugu.oneindia.com/rss/telugu-feature-fb.xml" },
            {"cinejosh", "http://www.cinejosh.com/rss-feed/0/telugu.html" }
        };

        private Utils _utils;
        SearchIndex _feedsIndex;
        SearchIndex _skippedUrlsIndex;

        public Feeds(Utils utils, SearchIndex feedsIndex, SearchIndex skippedUrlsIndex)
        {
            _utils = utils;
            _feedsIndex = feedsIndex;
            _skippedUrlsIndex = skippedUrlsIndex;
        }

        public Task<Feed[]> LoadFeeds(string feedUrl)
        {
            XDocument xDoc = XDocument.Load(feedUrl);
            var potentialFeeds = xDoc.Descendants("item");
            var feeds = potentialFeeds
                .Select(async 
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = await _utils.GetLandingPageUrl(t.Element("link").Value),
                            PublishedDate = DateTime.Parse(t.Element("pubDate").Value),
                            Description = Feeds.StripTagsRegexCompiled(t.Element("description").Value)
                        });
            return Task.WhenAll(feeds);
        }

        private string GetNextFeedSource(string lang)
        {
            string key;
           switch (lang)
            {
                case "english":
                    if (engCount == _englishfeeds.Keys.Count()) { engCount = 0; };
                    key = _englishfeeds.Keys.ToArray()[engCount];
                    engCount++;
                    return _englishfeeds[key];
                case "hindi":
                    if (hindiCount == _hindifeeds.Keys.Count()) { hindiCount = 0; };
                    key = _hindifeeds.Keys.ToArray()[hindiCount];
                    hindiCount++;
                    return _hindifeeds[key];
                case "marathi": 
                    if (marathiCount == _marathifeeds.Keys.Count()) { marathiCount = 0; };
                    key = _marathifeeds.Keys.ToArray()[marathiCount];
                    marathiCount++;
                    return _marathifeeds[key];
                case "telugu":
                    if (teluguCount == _telugufeeds.Keys.Count()) { teluguCount = 0; };
                    key = _telugufeeds.Keys.ToArray()[teluguCount];
                    teluguCount++;
                    return _telugufeeds[key];
                default: return _englishfeeds["ndtv"];
            }
        }

        public async Task<PostPreview> LoadNextFeed(string lang)
        {
            string feedsource = GetNextFeedSource(lang);
            var feeds = await LoadFeeds(feedsource);
            foreach (var feed in feeds)
            {
                try
                {
                    var sp1 = new SearchParameters()
                    {
                        Filter = String.Format("landingpageurl eq '{0}'", feed.Link.ToLower()),
                        IncludeTotalResultCount = true
                    };
                    var response1 = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*", sp1);

                    var sp2 = new SearchParameters()
                    {
                        Filter = String.Format("url eq '{0}'", feed.Link.ToLower()),
                        IncludeTotalResultCount = true
                    };

                    var response2 = await _skippedUrlsIndex.SearchAsync<SkippedUrlsIndexDoc>("*", sp2);

                    if (response1.Count > 0 || response2.Count > 0) { continue; }
                    try
                    {
                        return feed.ToPostPreview(_utils);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return feeds.First().ToPostPreview(_utils);
        }
    }
}
