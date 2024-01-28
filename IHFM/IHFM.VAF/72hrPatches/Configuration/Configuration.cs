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
        public MFIdentifier Patches_StartDate = "MFiles.Property.ScriptManagementStartDate";

        [MFPropertyDef]
        public MFIdentifier Patches_EndDate = "MFiles.Property.EndDate";

        [MFPropertyDef]
        public MFIdentifier Patches_Discontinued = "MFiles.Property.72hrsDiscontinued";

        [MFPropertyDef]
        public MFIdentifier Patches_Resident = "MFiles.Property.Resident";

        [MFPropertyDef]
        public MFIdentifier Patches_Timeslot = "MFiles.Property.72hrsTimeSlot";

        [MFPropertyDef]
        public MFIdentifier Patches_Patch = "MFiles.Property.Patch";
    }
}
