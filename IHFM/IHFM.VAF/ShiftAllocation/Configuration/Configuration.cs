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
        [MFClass]
        public MFIdentifier ShiftAllocation_Class = "MFiles.Class.ShiftAllocation";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_Resident = "MFiles.Property.ResidentHbc";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_StartDateTime = "MFiles.Property.StartDateTime";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_EndDateTime = "MFiles.Property.EndDateTime";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_StaffAttending = "MFiles.Property.StaffAttending";

        [DataMember]
        public string ShiftAllocation_MailLising { get; set; }
    }
}
