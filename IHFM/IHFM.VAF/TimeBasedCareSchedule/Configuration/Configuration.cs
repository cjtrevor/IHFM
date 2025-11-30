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
        public MFIdentifier TBCS_Assistant = "MFiles.Property.Assistant";

        [MFPropertyDef]
        public MFIdentifier TBCS_TimeBasedCareItem = "MFiles.Property.TimeBasedCareItem";
        [MFPropertyDef]
        public MFIdentifier TBCS_TbcScheduledTimes = "MFiles.Property.TbcScheduledTimes";

        [MFPropertyDef]
        public MFIdentifier TBCI_TBCType = "PD.Tbctype";

        [MFPropertyDef]
        public MFIdentifier TBCS_TimeBasedCareScheduleDropdown = "MFiles.Property.TimeBasedCareScedule";
    }
}
