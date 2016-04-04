using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts.Search;
using DataContracts.Client;

namespace NewsSwipesLibrary
{
    public static class UserCredentialsExtensionMethods
    {
        public static UserCredentialsIndexDoc ToUserCredentialsIndexDoc(this UserCredentials credentials, Config config)
        {
            var indexDoc = new UserCredentialsIndexDoc()
            {
                Id = Guid.NewGuid().ToString(),
                Email = credentials.Email.ToLower(),
                Password = credentials.Password.ToLower(),
                Language = credentials.Language.ToLower(),
                CanPost = false,
                Streams = config.GetStreams(credentials.Language.ToLower()).ToArray()
            };
            return indexDoc;
        }

        public static User ToUser(this UserCredentialsIndexDoc indexDoc)
        {
            return new User
            {
                Id = indexDoc.Id,
                Email = indexDoc.Email,
                Language = indexDoc.Language,
                Streams = indexDoc.Streams
            };
        }

        public static StorageIndexDoc ToStorageIndexDoc(this UserDeviceInfo deviceInfo)
        {
            return new StorageIndexDoc()
            {
                DeviceInfo = deviceInfo.JSON,
                Id = Guid.NewGuid().ToString()
            };
        }

        public static StorageIndexDoc ToStorageIndexDoc(this UserGeoInfo geoInfo)
        {
            return new StorageIndexDoc()
            {
                GeoInfo = geoInfo.JSON,
                Id = Guid.NewGuid().ToString()
            };
        }

        public static StorageIndexDoc ToStorageIndexDoc(this UserContactsInfo contactsInfo)
        {
            return new StorageIndexDoc()
            {
                GeoInfo = contactsInfo.JSON,
                Id = Guid.NewGuid().ToString()
            };
        }

        public static UserCredentialsIndexDoc ToUserIndexDoc(this User user)
        {
            return new UserCredentialsIndexDoc()
            {
                Id = user.Id,
                Email = user.Email,
                Language = user.Language,
                Name = user.Name,
                ProfileImage = user.ProfileImage,
                CanPost = user.CanPost,
                Streams = user.Streams
            };
        }

    }
}
