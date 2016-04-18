using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class UserCredentialsIndexDoc : SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "profileImage", NullValueHandling = NullValueHandling.Ignore)]
        public string ProfileImage { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "canPost", NullValueHandling = NullValueHandling.Ignore)]
        public bool CanPost { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "streams", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Streams { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "innetworkemails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] InNetworkEmailContacts { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "outofnetworkemails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] OutOfNetworkEmailContacts { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "innetworkphonenos", NullValueHandling = NullValueHandling.Ignore)]
        public string[] InNetworkPhoneNos { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "outofnetworkphonenos", NullValueHandling = NullValueHandling.Ignore)]
        public string[] OutOfNetworkPhoneNos { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "lastactivity", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastActivity { get; set; }
    }
}
