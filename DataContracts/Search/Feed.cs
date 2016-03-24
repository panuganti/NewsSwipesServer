using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class Feed
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime CreatedTime { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string LandingPageUrl { get; set; }

        [DataMember]
        public CardStyle CardStyle { get; set; }

        [DataMember]
        public string PostedBy { get; set; }

        [DataMember]
        public string[] SharedBy { get; set; }

        [DataMember]
        public string[] LikedBy { get; set; }
    }

    [DataContract]
    public enum CardStyle
    {
        [EnumMember]
        Horizontal = 0,
        [EnumMember]
        Vertical
    }
}
