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
        public MFIdentifier StartTimeTBC = "Mfiles.Property.StartTimeTbc";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ResidentLookup = "MFiles.Property.Resident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TBCADLLookup = "MFiles.Property.TBCADL";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TBCClinicLookup = "MFiles.Property.TBCClinic";

        [MFPropertyDef(Required = true)]
        public MFIdentifier EndTime = "MFiles.Property.EndTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TimeSpent = "MFiles.Property.TimeSpent";

        [MFPropertyDef(Required = true)]
        public MFIdentifier CostForService = "MFiles.Property.CostForService";

        [MFPropertyDef(Required = true)]
        public MFIdentifier AverageTime = "MFiles.Property.AverageTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier AverageCost = "MFiles.Property.AverageCost";
    }
}
