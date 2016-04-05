using System.Collections.Generic;
using System.Web.Http;
using DataContracts.Search;
using DataContracts.Client;
using NewsSwipesLibrary;
using Search;
using GoogleDatastore;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Azure.Search.Models;
using System.Web.Http.Cors;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeedController : Controller
    {
        SearchIndex _feedsIndex;
        SearchIndex _skippedUrlsIndex;
        Datastore _ds;
        Utils _utils;
        Feeds _feeds;

        public FeedController() : this(new Datastore(), IndexFactory.FeedsIndex, IndexFactory.SkippedUrlsIndex, new Utils())
        {
        }

        private FeedController(Datastore ds, SearchIndex feedsIndex, SearchIndex skippedUrlsIndex, Utils utils)
            :this(ds, feedsIndex, skippedUrlsIndex, new Feeds(utils, feedsIndex, skippedUrlsIndex), utils)
        {
        } 

        private FeedController(Datastore ds, SearchIndex feedsIndex, SearchIndex skippedUrlsIndex, Feeds feeds, Utils utils)
        {
            _ds = ds;
            _feedsIndex = feedsIndex;
            _skippedUrlsIndex = skippedUrlsIndex;
            _feeds = feeds;
            _utils = utils;
        }

        #region Test
        [HttpGet]
        [Route("feed/getfeed")]
        public string[] GetFeed()
        {
            return new[] { "version 0.0.1", "value2" };
        }
        #endregion Test

        #region Publishing
        [HttpPost]
        [Route("feed/PostArticle")]
        public async Task<bool> PostArticle([FromBody] UnpublishedPost post)
        {
            DocumentIndexResult uploadedDoc;
            try {
                if (post.ShouldSkip)
                {
                    SkippedUrlsIndexDoc skippedDoc = post.ToSkippedUrlsIndexDoc();
                    uploadedDoc = await _skippedUrlsIndex.UploadDocument(skippedDoc);
                    return uploadedDoc.Results.First().Succeeded;
                }
                FeedsIndexDoc doc = post.ToFeedsIndexDoc();
                uploadedDoc = await _feedsIndex.UploadDocument(doc);
                return uploadedDoc.Results.First().Succeeded;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("feed/PreviewArticle")]
        public PostPreview PreviewArticle([FromBody] string url)
        {
            return _utils.GetArticleData(url);
        }

        [HttpPost]
        [Route("feed/FetchFromFeedStream")]
        public async Task<PostPreview> FetchFromFeedStream([FromBody] string feedStream)
        {
            return await _feeds.LoadNextFeed(feedStream);
        }
        #endregion Publishing

        #region Feed
        [HttpGet]
        [Route("feed/getfeed/{streams}/{skip}")]
        public async Task<IEnumerable<PublishedPost>> GetNewsFeed(string streams, int skip)
        {
            string[] streamArray = streams.Split(',');
            List<PublishedPost> newsFeed = new List<PublishedPost>();
            try
            {
                bool first = true;
                string filterString = "";
                foreach (var stream in streamArray)
                {
                    if (first)
                    {
                        filterString = String.Format("streams/any(t: t eq '{0}')", stream);
                        first = false;
                    }
                    else
                    {
                        filterString = String.Format("{0} or streams/any(t: t eq '{1}')", filterString, stream);
                    }
                }
                var sp = new SearchParameters()
                {
                    Filter = filterString,
                    OrderBy = new List<string> { "createdtime desc" },
                    Top = 100,
                    Skip = skip,
                    IncludeTotalResultCount = true
                };
                DocumentSearchResult<FeedsIndexDoc> feeds = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*", sp);
                newsFeed.AddRange(feeds.Results.Select(t => t.Document.ToPublishedPost()));

                return newsFeed;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("feed/timeline/{userId}/{skip}")]
        public async Task<IEnumerable<PublishedPost>> GetTimeline(string userId, int skip)
        {
            try {
                var sp = new SearchParameters()
                {
                    Filter = string.Format("postedby eq '{0}'", userId),
                    OrderBy = new List<string> { "createddate" }
                };
                var feeds = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*", sp);
                return feeds.Results.Select(t=>t.Document.ToPublishedPost());
            }
            catch(Exception e)
            { throw e; }
        }

        #endregion Feed

        #region UserAction
        [HttpPost]
        [Route("feed/AddUserReaction")]
        public async Task<string[]> AddUserReaction([FromBody]UserReaction reaction)
        {
            try {
                var doc = await _feedsIndex.LookupDocument<FeedsIndexDoc>(reaction.ArticleId);
                var likedby = doc.LikedBy.ToList();
                var sharedby = doc.SharedBy.ToList();
                bool likeOrShare = true;
                switch (reaction.ReactionType)
                {
                    case "Like":
                        likedby.Add(reaction.UserId); break;
                    case "UnLike":
                        likedby.Remove(reaction.UserId); break;
                    case "ReTweet":
                        sharedby.Add(reaction.UserId); likeOrShare = false; break;
                    case "UnReTweet":
                        sharedby.Remove(reaction.UserId); likeOrShare = false; break;
                    default: throw new Exception("Unknown Reaction Type");
                }
                var indexDoc = reaction.ToFeedIndexDoc(likedby.ToArray(), sharedby.ToArray());
                var uploadedDoc = await _feedsIndex.UploadDocument(indexDoc);
                if (uploadedDoc.Results.First().Succeeded)
                {
                    if (likeOrShare) { return likedby.ToArray(); }
                    else { return sharedby.ToArray(); }
                }
                throw new Exception("Could not apply User Reaction");
            }
            catch(Exception e)
            {
                throw new Exception("Could not apply User Reaction");
            }
        }

        #endregion UserAction

        /*
[HttpPost]
[Route("feed/uploadimagefromurl")]
public bool UploadImageFromUrl([FromBody]UploadObject uploadObj)
{
    _ds.Upload(uploadObj.Filename, uploadObj.Url).Wait();
    return true;
}
*/


        /*
        [HttpGet]
        public int AddLike(string request)
        {
            // UserId, articleId
            return 0;
        }

        [HttpGet]
        public int RemoveLike(string request)
        {
            // UserId, articleId
            return 0;
        }

        [HttpGet]
        public int Retweet(string request)
        {
            // UserId, articleId
            return 0;
        }

        // POST api/feed
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        */
    }
}
