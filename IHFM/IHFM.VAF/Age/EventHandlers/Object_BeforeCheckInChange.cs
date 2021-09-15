using System;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, ObjectType = "MFiles.Object.Resident")]
        public void SetAge(EventHandlerEnvironment env)
        {
            AgeCalculationService ageCalculationService = new AgeCalculationService();
            ageCalculationService.RefreshAge(env.ObjVerEx, Configuration, false);
        }
    }
}
