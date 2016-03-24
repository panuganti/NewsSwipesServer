using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class Image
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "tags")]
        public string Tags { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "sourceUrl")]
        public string SourceUrl { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "dateadded")]
        public DateTime DateAdded { get; set; }
    }
}
