using System.Collections.Generic;
using System.Web.Http;
using DataContracts.Search;
using DataContracts.Client;
using Search;
using GoogleDatastore;
using System;

namespace NewsSwipesServer.Controllers
{
    public class FeedController : Controller
    {
        FeedsIndex _feeds;
        Datastore _ds;

        public FeedController() : this(new Datastore(), new FeedsIndex())
        {
        }

        public FeedController(Datastore ds, FeedsIndex feeds)
        {
            _ds = ds;
            _feeds = feeds;
        }

        #region Publishing
        [HttpGet]
        [Route("feed/PreviewArticle/{url}")]
        public PostPreview PreviewArticle(string url)
        {
            throw new NotImplementedException();
        }

        // POST feed/postarticle
        [HttpPost]
        [Route("feed/postarticle")]
        public bool PostArticle([FromBody]UnpublishedPost post)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("feed/fetchpostdata/{url}")]
        public PostPreview GetPostData(string url)
        {
            throw new NotImplementedException();
        }
        #endregion Publishing

        #region Feed
        [HttpGet]
        [Route("feed/getfeed/{lang}/{skip}")]
        public PublishedPost[] GetNewsFeed(string lang, int skip)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("feed/timeline/{userId}/{skip}")]
        public PublishedPost[] GetTimeline(string userI, int skip)
        {
            throw new NotImplementedException();
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
