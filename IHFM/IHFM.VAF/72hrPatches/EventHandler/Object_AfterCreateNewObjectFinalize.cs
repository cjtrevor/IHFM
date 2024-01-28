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
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.72hrPatches")]
        public void AfterCreateNew72hrPatches(EventHandlerEnvironment env)
        {
            StageFutureRecords(env.ObjVerEx);
        }

        private void StageFutureRecords(ObjVerEx patchRecord)
        {
            _72hrPatchesStagingService stagingService = new _72hrPatchesStagingService(Configuration);
            stagingService.StageFutureRecords(patchRecord);
        }
    }
}
