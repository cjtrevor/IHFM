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
        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_Resident = "MFiles.Property.ResidentHbc";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_StartDate = "MFiles.Property.Date";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_Time = "MFiles.Property.StartTime";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_EndDate = "MFiles.Property.EndDateText";
    }
}
