using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using DataContracts.Client;

namespace DataContracts.Search
{
    [DataContract]
    public class UserCredentialsIndexDoc : SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "profileImage")]
        public string ProfileImage { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "canPost")]
        public bool CanPost { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "streams")]
        public string[] Streams { get; set; }

        public UserCredentialsIndexDoc() { }

        public UserCredentialsIndexDoc(UserCredentials credentials)
        {
            Id = Guid.NewGuid().ToString(); // TODO: Change to assigning a guid
            Email = credentials.Email.ToLower();
            Password = credentials.Password.ToLower();
        }

        public User ToUser()
        {
            return new User
            {
                Id = Id,
                Email = Email,
                Language = Language
            };
        }
    }
}
