using System;
using System.Linq;
using DataContracts.Search;
using DataContracts.Client;
using System.Collections.Generic;
using NewsSwipesLibrary.ExtensionMethods;

namespace NewsSwipesLibrary
{
    public static class UserCredentialsExtensionMethods
    {
        public static UserCredentialsIndexDoc ToUserCredentialsIndexDoc(this UserSignupInfo signupInfo)
        {
            var indexDoc = new UserCredentialsIndexDoc()
            {
                Id = signupInfo.UserId.ToLower(),
                Email = string.Empty,
                Language = signupInfo.Language.ToLower(),
                CanPost = false,
                Streams = new string[] { }
            };
            return indexDoc;
        }

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

        public static User ToUser(this UserCredentialsIndexDoc indexDoc, IEnumerable<Stream> AllStreams)
        {
            var userStreams = AllStreams.Select(t => new Stream
            {
                Id = t.Id,
                Text = t.Text,
                Lang = t.Lang,
                IsAdmin = t.IsAdmin,
                UserSelected = indexDoc.Streams.Contains(t.ToIndexStream()),
                backgroundImageUrl = t.backgroundImageUrl
            }).ToArray();

            return new User
            {
                Id = indexDoc.Id,
                Email = indexDoc.Email,
                Language = indexDoc.Language,
                Name = indexDoc.Name,
                ProfileImage = indexDoc.ProfileImage,
                CanPost = indexDoc.CanPost,
                Streams = userStreams
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
