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

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : Controller
    {
        private SearchIndex _credentialsIndex;
        private SearchIndex _storageIndex;
        private Config _config;

        public UserController() : this(IndexFactory.CredentialsIndex, IndexFactory.StorageIndex, new Config())
        { }

        private UserController(SearchIndex credentialsIndex, SearchIndex storageIndex, Config config)
        {
            _credentialsIndex = credentialsIndex;
            _storageIndex = storageIndex;
            _config = config;
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
                    return docs.Results.First().Document.ToUser();
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
                        return storedCredentials.ToUser();
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
                var indexDoc = credentials.ToUserCredentialsIndexDoc(_config);
                var uploadedDoc = await _credentialsIndex.UploadDocument(indexDoc);
                if (!uploadedDoc.Results.First().Succeeded)
                {
                    throw new Exception(string.Format("Sorry, could not sign you up. Please try again"));
                }

                // If Signup success
                return indexDoc.ToUser();
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
            var uploadedDoc = await _credentialsIndex.UpdateDocument(user.ToUserIndexDoc());
            return uploadedDoc.Results.First().Succeeded;
        }

        [HttpPost]
        [Route("user/UpdateUserDeviceInfo")]
        public async Task<bool> UpdateUserDeviceInfo([FromBody]UserDeviceInfo deviceInfo)
        {
            var uploadedDoc = await _storageIndex.UpdateDocument(deviceInfo.ToStorageIndexDoc());
            return uploadedDoc.Results.First().Succeeded;
        }

        [HttpPost]
        [Route("user/UpdateUserGeoInfo")]
        public async Task<bool> UpdateUserGeoInfo([FromBody]UserGeoInfo geoInfo)
        {
            var uploadedDoc = await _storageIndex.UpdateDocument(geoInfo.ToStorageIndexDoc());
            return uploadedDoc.Results.First().Succeeded;
        }

        [HttpPost]
        [Route("user/UpdateUserContactList")]
        public async Task<bool> UpdateUserContactList([FromBody]UserContactsInfo contactsInfo)
        {
            var uploadedDoc = await _storageIndex.UpdateDocument(contactsInfo.ToStorageIndexDoc());
            return uploadedDoc.Results.First().Succeeded;
        }

        [HttpGet]
        [Route("user/GetStreams/{userId}")]
        public async Task<IEnumerable<Stream>> GetStreams(string userId)
        {
            var streams = _config.AllStreams;
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            var userSelectStreams = new List<Stream>();
            foreach(var stream in streams)
            {
                Stream s = stream;
                if (!user.Streams.Contains(String.Format("{0}_{1}", s.Lang.ToLower(), s.Text.ToLower())))
                {
                    s.UserSelected = false;
                }
                userSelectStreams.Add(s);
            }
            return userSelectStreams;
        }

        #region Contacts
        [HttpGet]
        [Route("user/FetchContacts/{userId}")]
        public async Task<IEnumerable<UserContact>> FetchContacts(string userId)
        {
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
           
        }

        [HttpPost]
        [Route("user/UpdateContacts/{userId}")]
        public async Task<bool> UpdateContacts([FromBody]IEnumerable<UserContact> userContacts, string userId)
        {
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>("");
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("user/UpdateContact")]
        public async Task<bool> UpdateContact([FromBody]UserContact userContact)
        {
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("user/DeleteContact")]
        public async Task<bool> DeleteContact([FromBody]UserContact userContact)
        {
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("user/UnFollowContact")]
        public async Task<bool> UnFollowContact([FromBody]UserContact userContact)
        {
            var user = await _credentialsIndex.LookupDocument<UserCredentialsIndexDoc>(userId);
            throw new NotImplementedException();
        }

        #endregion Contacts
    }
}
