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
        public MFIdentifier GCSRequired = "MFiles.Property.GcsRequired";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BestMotorResponse = "MFiles.Property.BestMotorResponse";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BestVerbalResponse = "MFiles.Property.BestVerbalResponse";
        [MFPropertyDef(Required = true)]
        public MFIdentifier EyeOpeningResponse = "MFiles.Property.EyeOpeningResponse";
    }
}
