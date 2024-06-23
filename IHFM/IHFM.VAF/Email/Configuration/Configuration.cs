using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [DataMember]
        public string MRContractor_SubjectLine { get; set; }

        [DataMember]
        public string MRContractor_BodyLine { get; set; }

        [DataMember]
        public string Email_SMTP { get; set; }

        [DataMember]
        public string Email_Username { get; set; }

        [DataMember]
        public string Email_Password { get; set; }

        [DataMember]
        public string Email_FromAddress { get; set; }

        [DataMember]
        public string Email_FriendlyAddress { get; set; }
    }
}
