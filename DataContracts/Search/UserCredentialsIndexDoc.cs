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
    }

    [DataContract]
    public class StorageIndexDoc: SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "deviceinfo", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceInfo { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "geoinfo", NullValueHandling = NullValueHandling.Ignore)]
        public string GeoInfo { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "contactinfo", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactInfo { get; set; }
    }
}
