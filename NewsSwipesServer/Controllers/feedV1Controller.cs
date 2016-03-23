using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSwipesServer.Models;

namespace NewsSwipesServer.Controllers
{
    public class feedV1Controller : ApiController
    {
        // GET api/feedV1/action/request
        [HttpGet]
        public IEnumerable<string> myAction(string request)
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> GetNewsFeed(string request)
        {
            return new[] { "value1", "value2" };
        }

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
    }
}
