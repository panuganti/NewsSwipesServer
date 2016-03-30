using System;
using System.Runtime.Serialization;

namespace DataContracts.Search
{
    [DataContract]
    public class FeedsIndexDoc : SearchDoc
    {
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
        public string CardStyle { get; set; }

        [DataMember]
        public string PostedBy { get; set; }

        [DataMember]
        public string[] SharedBy { get; set; }

        [DataMember]
        public string[] LikedBy { get; set; }

        [DataMember]
        public string[] Streams { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Tags { get; set; }
    }
}
