using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class SearchDoc
    {
        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
