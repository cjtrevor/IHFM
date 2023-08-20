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
        //[PropertyCustomValue("MFiles.Property.CarePlanNotes")]
        //public TypedValue SetCareplanNotesValue(PropertyEnvironment env)
        //{
        //    CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
        //    int lookupId = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID();

        //    ObjVerEx careplan = searchService.GetResidentCarePlan(lookupId);

        //    string output = careplan == null ? "" : $"{careplan.GetPropertyText(Configuration.Careplan_CpDietAndFeeding)}" +
        //        $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpToilet)}";

        //    TypedValue calculated = new TypedValue();
        //    calculated.SetValue(MFDataType.MFDatatypeText, output);

        //    return calculated;
        //}
    }
}
