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
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.72hrPatches")]
        public void After72hrPatchesCheckInChangesFinalized(EventHandlerEnvironment env)
        {
            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);
            
            if(changes.Changed.Any(x=> x.PropertyDef == Configuration.Patches_Discontinued) && env.ObjVerEx.HasValue(Configuration.Patches_Discontinued) 
                && env.ObjVerEx.GetProperty(Configuration.Patches_Discontinued).GetValue<bool>())
            {
                _72hrPatchesStagingService stagingService = new _72hrPatchesStagingService(Configuration);
                stagingService.ClearFutureRecords(env.ObjVerEx);
            }
        }
    }
}
