using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataContracts;

namespace NewsSwipesServer.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user/GetUserInfo/request
        [HttpGet]
        public User GetUserInfo(string request)
        {
            throw new NotImplementedException();
        }

        public CredentialsValidation ValidateCredentials(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        #region PUT and DELETE
        /*
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        */
        #endregion PUT and DELETE
    }
}
