using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DataContracts.Search
{
    [DataContract]
    public class FeedsIndexDoc : SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "createdtime")]
        public DateTime CreatedTime { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "imageurl")]
        public string ImageUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "landingpageurl")]
        public string LandingPageUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "cardstyle")]
        public string CardStyle { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "postedby")]
        public string PostedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "sharedby")]
        public string[] SharedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "likedby")]
        public string[] LikedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "streams")]
        public string[] Streams { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "OriginalLink")]
        public string OriginalLink { get; set; }
    }
}
