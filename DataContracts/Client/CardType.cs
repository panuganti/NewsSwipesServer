using System.Runtime.Serialization;

namespace DataContracts
{
    [DataContract]
    public enum CardType
    {
        [EnumMember(Value = "Default")]
        Default = 0,
        [EnumMember]
        Vertical = 1
    }
}
