using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts.Search;
using DataContracts.Client;
using Newtonsoft.Json;

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
                Streams = config.AllStreams.Where(s => s.Lang.ToLower() == credentials.Language.ToLower())
                            .Select(s => String.Format("{0}_{1}", s.Lang.ToLower(), s.Text.ToLower())).ToArray()
            };
            return indexDoc;
        }

        public static User ToUser(this UserCredentialsIndexDoc indexDoc)
        {
            // TODO: Have seperate code for converters (other aspects of stream missing), lower/upper case is missing
            Stream[] streams = indexDoc.Streams.Select(t =>
            {
                var splits = t.Split('_');
                return new Stream { Lang = splits[0], Text = splits[1] };
            }).ToArray();

            return new User
            {
                Id = indexDoc.Id,
                Email = indexDoc.Email,
                Language = indexDoc.Language,
                Streams = streams
            };
        }

        public static UserCredentialsIndexDoc ToUserIndexDoc(this User user)
        {
            string[] streams = user.Streams.Select(t=> String.Format("{0}_{1}", t.Lang.ToLower(), t.Text.ToLower())).ToArray();
            return new UserCredentialsIndexDoc()
            {
                Id = user.Id,
                Email = user.Email,
                Language = user.Language,
                Name = user.Name,
                ProfileImage = user.ProfileImage,
                CanPost = user.CanPost,
                Streams = streams
            };
        }

    }
}
