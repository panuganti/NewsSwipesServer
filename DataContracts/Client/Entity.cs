using System.Runtime.Serialization;

namespace DataContracts.Client
{
    [DataContract]
    public class Entity
    {
        [DataMember]
        public string Id { get; set; }
    }

    [DataContract]
    public class EntityWithText : Entity
    {
        [DataMember]
        public string Text { get; set; }
    }
}
