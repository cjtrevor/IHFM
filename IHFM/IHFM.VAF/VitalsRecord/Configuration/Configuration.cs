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
        public MFIdentifier Vitals_Temperature = "MFiles.Property.Temperature";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_SystolicBP = "MFiles.Property.SystolicBP";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_DiastolicBP = "MFiles.Property.DiastolicBP";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_HeartRate = "MFiles.Property.HeartRate";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_Weight = "MFiles.Property.Weight";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_HGT = "MFiles.Property.HGT";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_Saturation = "MFiles.Property.Saturation";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_HB = "MFiles.Property.HB";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Vitals_Monthly = "MFiles.Property.Monthly";
    }
}
