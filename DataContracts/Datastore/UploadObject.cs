using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    [DataContract]
    public class UploadObject
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Filename { get; set; }
    }
}
