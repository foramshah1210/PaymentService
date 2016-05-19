using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Common
{
    public class UnexpectedServiceFault
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string StackTrace { get; set; }
        [DataMember]
        public string Target { get; set; }
        [DataMember]
        public string Source { get; set; }
    }
}