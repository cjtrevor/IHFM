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
        public string SQLExport_Server { get; set; }

        [DataMember]
        public string SQLExport_Database { get; set; }
    }
}
