using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class ImagesIndexDoc : SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "sourceUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "dateadded", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateAdded { get; set; }
    }
}
