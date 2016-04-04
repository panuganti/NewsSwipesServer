using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class SkippedUrlsIndexDoc : SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }
}
