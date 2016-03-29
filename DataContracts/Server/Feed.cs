using System;
using System.Runtime.Serialization;

namespace DataContracts.Server
{
    [DataContract]
    public class Feed
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public DateTime PublishedDate { get; set; }
    }
}
