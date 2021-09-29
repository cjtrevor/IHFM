using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.GcsScore")]
        public TypedValue SetGcsScore(PropertyEnvironment env)
        {
            GcsCalculationService gcsCalculationService = new GcsCalculationService(Configuration);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, gcsCalculationService.GetGcsScore(env.ObjVerEx));

            return calculated;
        }
    }
}
