using System.Collections.Generic;
using System.Web.Http;
using DataContracts;
using Search;
using GoogleDatastore;

namespace NewsSwipesServer.Controllers
{
    public class FeedController : ApiController
    {
        Feed _feeds;
        Datastore _ds;

        public FeedController() : this(new Datastore(), new Feed())
        {
        }

        public FeedController(Datastore ds, Feed feeds)
        {
            _ds = ds;
            _feeds = feeds;
        }

        // POST feed/postarticle
        [HttpPost]
        [Route("feed/postarticle")]
        public IEnumerable<string> myAction([FromBody]Article article)
        {
            var batch = _feeds.UploadDocuments(new Article[] { article });
            return new[] { "value2" };
        }

        [HttpPost]
        [Route("feed/uploadimagefromurl")]
        public bool UploadImageFromUrl([FromBody]UploadObject uploadObj)
        {
            _ds.Upload(uploadObj.Filename, uploadObj.Url).Wait();
            return true;
        }

        [HttpGet]
        [Route("feed/getfeed/{request}")]
        public IEnumerable<string> GetNewsFeed(string request)
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("feed/fetchpostdata/{url}")]
        public string GetPostData(string url)
        {
            return "hell world";
        }

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
