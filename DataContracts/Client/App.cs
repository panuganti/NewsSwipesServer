using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataContracts.Client
{
    [DataContract]
    public class ConfigData
    {
        [DataMember]
        public Dictionary<string, Dictionary<string, string>> Labels { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}
