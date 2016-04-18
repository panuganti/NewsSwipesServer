using System;
using System.Web.Http;
using DataContracts.Client;
using DataContracts.Search;
using Search;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http.Cors;
using NewsSwipesLibrary;
using System.Collections.Generic;
using GoogleDatastore;
using Newtonsoft.Json;
using NewsSwipesLibrary.ExtensionMethods;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : Controller
    {
        private SearchIndex _credentialsIndex;
        private Config _config;
        private Datastore _ds; 

        public UserController() : this(IndexFactory.CredentialsIndex, new Config(), new Datastore())
        { }

        private UserController(SearchIndex credentialsIndex, Config config, Datastore ds)
        {
            _credentialsIndex = credentialsIndex;
            _config = config;
            _ds = ds;
        }

        [HttpGet]
        [Route("user/GetUserInfo/{userId}")]
        public async Task<User> GetUserInfo(string userId)
        {
            try
            {
                var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*", String.Format("id eq '{0}'", userId.ToLower()));
                if (docs.Count != 0)
                {
                    var userIndexDoc = docs.Results.First().Document;
                    var user =  userIndexDoc.ToUser(_config);
                    return user;
                }
                throw new Exception("UserId invalid");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET api/user/GetUserInfo/request
        [HttpGet]
        [Route("user/CheckIfEmailExists")]
        public async Task<bool> CheckIfEmailExists(string email)
        {
            try
            {
                var docs = await _credentialsIndex.Search<UserCredentialsIndexDoc>("*",
                                        String.Format("email eq '{0}'", email.ToLower()));
                if (docs.Count != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/ValidateCredentials")]
        public async Task<User> ValidateCredentials([FromBody]UserCredentials credentials)
        {
            try
            {
                var docs = await _credentialsIndex
                    .Search<UserCredentialsIndexDoc>("*", String.Format("email eq '{0}'", credentials.Email.ToLower()));
                if (docs.Count == 1)
                {
                    var storedCredentials = docs.Results.First().Document;
                    if (storedCredentials.Password == credentials.Password)
                    {
                        return storedCredentials.ToUser(_config);
                    }
                    throw new Exception("Password Incorrect");
                }
                throw new Exception("None or Duplicate Users found");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST api/values
        [HttpPost]
        [Route("user/SignUp")]
        public async Task<User> SignUp([FromBody]UserCredentials credentials)
        {
            try
            {
                bool isAlreadySignedUp = await CheckIfEmailExists(credentials.Email);
                if (isAlreadySignedUp)
                {
                    throw new Exception(string.Format("Email {0} already a user", credentials.Email));
                }
                // TODO: Check if password has min requirements
                var indexDoc = credentials.ToUserCredentialsIndexDoc();

                // Get default streams... and get user streams from contacts
                indexDoc.Streams = _config.AllStreams.Where(t => t.Lang.ToLower() == credentials.Language.ToLower())
                                    .Select(t => String.Format("{0}_{1}", t.Lang.ToLower(), t.Text.ToLower())).ToArray();
                var uploadedDoc = await _credentialsIndex.UploadDocument(indexDoc);
                if (!uploadedDoc.Results.First().Succeeded)
                {
                    throw new Exception(string.Format("Sorry, could not sign you up. Please try again"));
                }

                // If Signup success
                return indexDoc.ToUser(_config);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // POST api/values
        [HttpPost]
        [Route("user/UpdateUserProfile")]
        public async Task<bool> UpdateUserProfile([FromBody]User user)
        {
            try
            {
                var uploadedDoc = await _credentialsIndex.UpdateDocument(user.ToUserIndexDoc());
                return uploadedDoc.Succeeded;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region UserInfo
        [HttpPost]
        [Route("user/UpdateUserDeviceInfo")]
        public async Task<bool> UpdateUserDeviceInfo([FromBody]UserDeviceInfo deviceInfo)
        {
            try {
                return await _ds.UploadStorageInfoAsync(
                    deviceInfo.UserId == null ? Guid.NewGuid().ToString() : deviceInfo.UserId, JsonConvert.SerializeObject(deviceInfo));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/UpdateUserGeoInfo")]
        public async Task<bool> UpdateUserGeoInfo([FromBody]UserGeoInfo geoInfo)
        {
            try { 
            return await _ds.UploadStorageInfoAsync(geoInfo.UserId == null ? Guid.NewGuid().ToString() : geoInfo.UserId, JsonConvert.SerializeObject(geoInfo));
            }
            catch (Exception e)
            {
                throw e; 
            }
        }

        [HttpPost]
        [Route("user/UpdateUserContactList")]
        public async Task<bool> UpdateUserContactList([FromBody]UserContactsInfo contactsInfo)
        {
            try { 
            return await _ds.UploadStorageInfoAsync(contactsInfo.UserId == null ? Guid.NewGuid().ToString() : contactsInfo.UserId, JsonConvert.SerializeObject(contactsInfo));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion UserInfo

        [HttpGet]
        [Route("user/GetStreams/{userId}")]
        public async Task<IEnumerable<Stream>> GetStreams(string userId)
        {
            try
            {
                var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
                var streams = _config.AllStreams.Where(s => s.Lang.ToLower() == user.Language.ToLower());
                return streams.Select(t => new Stream
                {
                    Id = t.Id,
                    Text = t.Text,
                    Lang = t.Lang,
                    IsAdmin = t.IsAdmin,
                    UserSelected = user.Streams.Contains(t.ToIndexStream()),
                    backgroundImageUrl = t.backgroundImageUrl
                }).ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/UpdateStreams/{userId}")]
        public async Task<bool> UpdateStreams(string userId, [FromBody] Stream[] updatedStreams)
        {
            try
            {
                var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
                user.Streams = updatedStreams.Select(t => t.ToIndexStream()).ToArray();
                var result = await _credentialsIndex.UpdateDocument(user);
                return result.Succeeded;
                // TODO: Add support for UserContacts Followers
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Contacts
        [HttpGet]
        [Route("user/FetchContacts/{userId}")]
        public async Task<IEnumerable<UserContact>> FetchContacts(string userId)
        {
            try { 
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/UpdateContacts/{userId}")]
        public async Task<bool> UpdateContacts([FromBody]IEnumerable<UserContact> userContacts, string userId)
        {
            try { 
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>("");
            throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/UpdateContact/{userId}")]
        public async Task<bool> UpdateContact([FromBody]UserContact userContact, string userId)
        {
            try { 
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/DeleteContact/{userId}")]
        public async Task<bool> DeleteContact([FromBody]UserContact userContact, string userId)
        {
            try { 
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("user/UnFollowContact/{userId}")]
        public async Task<bool> UnFollowContact([FromBody]UserContact userContact, string userId)
        {
            try { 
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Contacts
    }
}
