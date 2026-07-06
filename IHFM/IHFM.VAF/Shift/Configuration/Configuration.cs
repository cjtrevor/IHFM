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
        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier ShiftStartTime = "MFiles.Property.ShiftStartTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Shift = "MFiles.Property.Shift";

        [MFPropertyDef]
        public MFIdentifier AutoShift = "MFiles.Property.Autoshift";
    }
}
