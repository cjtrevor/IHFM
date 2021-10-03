using System;
using MFiles.VAF.Common;
using MFilesAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.IntakeAndOutput")]
        public void UpdateTotalShiftInputOutput(EventHandlerEnvironment env)
        {
            InputOutputService inputOutputService = new InputOutputService(env.Vault, Configuration);
            inputOutputService.UpdateInputOutputForShift(env.ObjVerEx);
        }
    }
}
