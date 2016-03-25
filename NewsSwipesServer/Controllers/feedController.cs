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

namespace NewsSwipesServer.Controllers
{
    public class FeedController : Controller
    {
        FeedsIndex _feedsIndex;
        Datastore _ds;
        Utils _utils;

        public FeedController() : this(new Datastore(), new FeedsIndex(), new Utils())
        {
        }

        public FeedController(Datastore ds, FeedsIndex feeds, Utils utils)
        {
            _ds = ds;
            _feedsIndex = feeds;
            _utils = utils;
        }

        #region Publishing
        [HttpGet]
        [Route("feed/PreviewArticle/{url}")]
        public PostPreview PreviewArticle(string url)
        {
            return _utils.GetArticleData(url);
        }

        // POST feed/postarticle
        [HttpPost]
        [Route("feed/postarticle")]
        public async Task<bool> PostArticle([FromBody]UnpublishedPost post)
        {
            FeedsIndexDoc doc = post.ToFeedsIndexDoc();
            var uploadedDoc = await _feedsIndex.UploadDocument(doc);
            return uploadedDoc.Results.First().Succeeded;
        }
        #endregion Publishing

        #region Feed
        [HttpGet]
        [Route("feed/getfeed/{lang}/{skip}")]
        public async Task<DocumentSearchResult<FeedsIndexDoc>> GetNewsFeed(string lang, int skip)
        {
            var sp = new SearchParameters() {
                Filter = String.Format("lang eq {0}", lang)
            };
            var feeds = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*",sp);
            return feeds;
        }

        [HttpGet]
        [Route("feed/timeline/{userId}/{skip}")]
        public async Task<DocumentSearchResult<FeedsIndexDoc>> GetTimeline(string userId, int skip)
        {
            var sp = new SearchParameters()
            {
                Filter = string.Format("createdby eq {0}", userId),
                OrderBy = new List<string> { "createddate" }
            };
            var feeds = await _feedsIndex.SearchAsync<FeedsIndexDoc>("*", sp);
            return feeds;
        }

        #endregion Feed

        #region UserAction
        [HttpPost]
        [Route("feed/postarticle")]
        public bool AddUserReaction([FromBody]UserReaction reaction)
        {
            throw new NotImplementedException();
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
