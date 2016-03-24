using System;
using System.Runtime.Serialization;

namespace DataContracts
{
    [DataContract]
    public class ArticleData
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string[] ImageUrls { get; set; }
        [DataMember]
        public DateTime ArticleDate { get; set; }
    }
}
