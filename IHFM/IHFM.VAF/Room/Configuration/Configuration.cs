using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef(Required = true)]
        public MFIdentifier Vacant = "MFiles.Property.Vacant";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Room_Zone = "MFiles.Property.Zone";

        [MFObjType(Required = true)]
        public MFIdentifier Room_Object = "MFiles.Object.Room";
    }
}
