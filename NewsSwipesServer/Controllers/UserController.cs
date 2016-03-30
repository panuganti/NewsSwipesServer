﻿using System;
using System.Web.Http;
using DataContracts.Client;
using DataContracts.Search;
using Search;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http.Cors;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : Controller
    {
        private CredentialsIndex _credentialsIndex;
        
        public UserController()
        { }

        private UserController(CredentialsIndex credentialsIndex)
        {
            _credentialsIndex = credentialsIndex;
        }

        // GET api/user/GetUserInfo/request
        [HttpGet]
        [Route("user/CheckIfEmailExists/{email}")]
        public async Task<bool> CheckIfEmailExists(string email)
        {
            var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("email eq {0}", email));
            if (docs.Count != 0)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("user/ValidateCredentials")]
        public async Task<User> ValidateCredentials([FromBody]UserCredentials credentials)
        {
            var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("email eq {0}", credentials.Email));
            if (docs.Count == 1)
            {
                var storedCredentials = docs.Results.First().Document;
                if (storedCredentials.Password == credentials.Password)
                {
                    return storedCredentials.ToUser();
                }
                throw new Exception("Password Incorrect");
            }
            throw new Exception("None or Duplicate Users found");
        }

        // POST api/values
        [HttpPost]
        [Route("user/SignUp")]
        public async Task<User> SignUp([FromBody]UserCredentials credentials)
        {
            bool isAlreadySignedUp = await CheckIfEmailExists(credentials.Email);
            if (isAlreadySignedUp)
            {
                throw new Exception(string.Format("Email {0} already a user", credentials.Email));
            }
            // TODO: Check if password has min requirements
            var uploadedDoc = await _credentialsIndex.UploadDocument(new UserCredentialsIndexDoc(credentials));
            if (!uploadedDoc.Results.First().Succeeded)
            {
                throw new Exception(string.Format("Sorry, could not sign you up. Please try again", credentials.Email));
            }

            // If Signup success
            var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("email eq {0}", credentials.Email));
            return docs.Results.First().Document.ToUser();
        }

        // POST api/values
        [HttpPost]
        [Route("user/UpdateUserProfile")]
        public async Task<bool> UpdateUserProfile([FromBody]User user)
        {
            var uploadedDoc = await _credentialsIndex.UpdateDocument(user.ToUserIndexDoc());
            return uploadedDoc.Results.First().Succeeded;
        }

        [HttpGet]
        [Route("user/GetStreams/{userId}")]
        public async Task<IEnumerable<Stream>> GetStreams(string userId)
        {
            var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("userid eq {0}", userId.ToLower()));
            if (docs.Count == 1)
            {
                var storedUser = docs.Results.First().Document;
                return storedUser.Streams;
            }
        }

        [HttpGet]
        [Route("user/GetUserInfo/{userId}")]
        public async Task<User> GetUserInfo(string userId)
        {
            var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("userid eq {0}", userId.ToLower()));
            if (docs.Count == 1)
            {
                var storedUser = docs.Results.First().Document;
                return storedUser.ToUser();
            }
        }
    }
}
