using MFiles.VAF.Configuration;
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
        public string Email_SMTP { get; set; }

        [DataMember]
        public string Email_Username { get; set; }

        [DataMember]
        public string Email_Password { get; set; }

        [DataMember]
        public string Email_FromAddress { get; set; }

        [DataMember]
        public int Email_Port { get; set; }

        [DataMember]
        public bool Email_EnableSsl { get; set; }

        [DataMember]
        public string Email_PickupDirectoryLocation { get; set; }
    }
}
