using System;
using System.Linq;
using DataContracts.Search;
using DataContracts.Client;
using System.Collections.Generic;

namespace NewsSwipesLibrary
{
    public static class UserCredentialsExtensionMethods
    {
        public static UserCredentialsIndexDoc ToUserCredentialsIndexDoc(this UserCredentials credentials)
        {
            var indexDoc = new UserCredentialsIndexDoc()
            {
                Id = Guid.NewGuid().ToString(),
                Email = credentials.Email.ToLower(),
                Password = credentials.Password.ToLower(),
                Language = credentials.Language.ToLower(),
                CanPost = false,
                Streams = new string[] {}
            };
            return indexDoc;
        }

        public static User ToUser(this UserCredentialsIndexDoc indexDoc, Config config)
        {
            // TODO: Have seperate code for converters (other aspects of stream missing), lower/upper case is missing
            // streams, user streams
            List<Stream> streams = indexDoc.Streams.Select(t =>
            {
                var splits = t.Split('_');
                if (splits.Length == 2)
                {
                    return new Stream { Lang = splits[0], Text = splits[1], UserSelected = true };
                }
                return null; // TODO:
            }).ToList();

            var langStreams = config.AllStreams.Where(s => s.Lang.ToLower() == indexDoc.Language.ToLower());

            return new User
            {
                Id = indexDoc.Id,
                Email = indexDoc.Email,
                Language = indexDoc.Language,
                Streams = langStreams.ToArray(),
                CanPost = indexDoc.CanPost
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
