using System.Runtime.Serialization;

namespace DataContracts.Client
{
    [DataContract]
    public class Stream : EntityWithText
    {
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
        [DataMember]
        public bool UserSelected { get; set; }
        [DataMember]
        public string backgroundImageUrl { get; set; }
    }
}
