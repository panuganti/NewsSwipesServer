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
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "createdtime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedTime { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "imageurl", NullValueHandling = NullValueHandling.Ignore)]
        public string ImageUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "landingpageurl", NullValueHandling = NullValueHandling.Ignore)]
        public string LandingPageUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "cardstyle", NullValueHandling = NullValueHandling.Ignore)]
        public string CardStyle { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "postedby", NullValueHandling = NullValueHandling.Ignore)]
        public string PostedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "sharedby", NullValueHandling = NullValueHandling.Ignore)]
        public string[] SharedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "likedby", NullValueHandling = NullValueHandling.Ignore)]
        public string[] LikedBy { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "streams", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Streams { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "OriginalLink", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalLink { get; set; }
    }
}
