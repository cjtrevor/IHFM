using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MedsGiven", Priority = 100)]
        public void BeforeCreateNewMedsGiven(EventHandlerEnvironment env)
        {
            SetMedsToGive(env);
        }

        public void SetMedsToGive(EventHandlerEnvironment env)
        {
            //Get ScriptManagementObject
            ScriptManagementSearchService searchService = new ScriptManagementSearchService(env.Vault, Configuration);
            ObjVerEx script = searchService.GetScriptManagementObject(env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID());

            //No object show error
            if(script == null)
            {
                throw new Exception("There is no active script for this resident in the system");
            }

            // Check if anymeds for now +- 30min
            ScriptManagementUtilityService scriptUtilityService = new ScriptManagementUtilityService(Configuration);

            MFIdentifier slotToCheck = scriptUtilityService.GetScheduledTimeToCheck(DateTime.Now);

            if(slotToCheck == null)
            {
                throw new Exception("The current time has no valid slot.");
            }

            List<Lookup> medsToGive = searchService.GetMedsScheduledForTimeSlot(slotToCheck, script);

            if (medsToGive.Count == 0)
            {
                throw new Exception("There is no medicine scheduled for the current time slot");
            }

            //Add to meds dropdown
            foreach  (Lookup med in medsToGive)
            {
                env.ObjVerEx.AddLookup(Configuration.MedsOnScript, med.ToLatestObjVer(env.Vault));
            }

            env.ObjVerEx.SaveProperties();
        }
    }
}
