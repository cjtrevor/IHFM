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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.Resident")]
        public void CalculateAge(EventHandlerEnvironment env)
        {
            AgeCalculationService ageCalculationService = new AgeCalculationService();
            ageCalculationService.RefreshAge(env.ObjVerEx, Configuration, false);
        }
    }
}
