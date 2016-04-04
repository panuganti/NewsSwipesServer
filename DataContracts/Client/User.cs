using System.Runtime.Serialization;
using DataContracts.Search;

namespace DataContracts.Client
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ProfileImage { get; set; }
        [DataMember]
        public bool CanPost { get; set; }
        [DataMember]
        public string[] Streams { get; set; }
    }

    [DataContract]
    public class UserDeviceInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string JSON { get; set; }
    }

    [DataContract]
    public class UserContactsInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string JSON { get; set; }
    }

    [DataContract]
    public class UserGeoInfo
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string JSON { get; set; }
    }

    [DataContract]
    public class UserNotification
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public bool IsRead { get; set; }
    }

    [DataContract]
    public class UserCredentials
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Language { get; set; }
    }

    [DataContract]
    public class CredentialsValidation
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public bool Validated { get; set; }
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class VersionInfo
    {
        [DataMember]
        public string LatestVersion { get; set; }
        [DataMember]
        public string MinSupportedVersion { get; set; }
    }

    [DataContract]
    public class UserReaction
    {
        [DataMember]
        public string ArticleId { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string ReactionType { get; set; }
    }

    [DataContract]
    public enum ReactionType
    {
        [EnumMember]
        Like,
        [EnumMember]
        ReTweet,
        [EnumMember]
        UnLike,
        [EnumMember]
        UnReTweet
    }
}
