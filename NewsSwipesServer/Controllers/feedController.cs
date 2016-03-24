using System.Collections.Generic;
using System.Web.Http;
using DataContracts;
using System.Runtime.Serialization;
using Search;
using Microsoft.Azure.Search;
using System;

namespace NewsSwipesServer.Controllers
{
    public class FeedController : ApiController
    {
        Feeds _feeds;
        
        public FeedController()
        {
            _feeds = new Feeds();
        }

        // POST feed/postarticle
        [HttpPost]
        [Route("feed/postarticle")]
        public IEnumerable<string> myAction([FromBody]Article article)
        {
            var batch = _feeds.UploadDocuments(new Article[] { article });
            return new[] { "value2" };
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
