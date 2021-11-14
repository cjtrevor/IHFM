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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.CarePlan",Priority = 100)]
        public void CheckCarePlanExists(EventHandlerEnvironment env)
        {
            CarePlanSearchService carePlanSearchService = new CarePlanSearchService(env.Vault, Configuration);
            int residentId = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID();

            if(carePlanSearchService.GetResidentCarePlan(residentId) != null)
            {
                throw new Exception("This resident already has a care plan in the system. Multiple care plans are not allowed.");
            }
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.CarePlan", Priority = 99)]
        public void SetResidentCarePlanFlag(EventHandlerEnvironment env)
        {
            ResidentPropertyService service = new ResidentPropertyService(env.Vault, Configuration);
            Lookup resident = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();

            service.SetCarePlanFlag(resident);
        }
    }
}
