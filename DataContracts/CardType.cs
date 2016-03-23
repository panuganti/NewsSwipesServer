using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
