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

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.PrnMedsGiven", Priority = 100)]
        public void BeforeCreateNewPRNMedsGiven(EventHandlerEnvironment env)
        {
            SetPRNMedsToGive(env);
        }

        public void SetPRNMedsToGive(EventHandlerEnvironment env)
        {
            ScriptManagementSearchService searchService = new ScriptManagementSearchService(env.Vault, Configuration);
            List<ObjVerEx> activeScripts = searchService.GetAllScriptManagementObject(env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID());

            //No object show error
            if (activeScripts.Count == 0)
            {
                throw new Exception("There is no active script for this resident in the system");
            }

            ScriptManagementUtilityService scriptUtilityService = new ScriptManagementUtilityService(Configuration);
            MFIdentifier slotToCheck = scriptUtilityService.GetScheduledTimeToCheck(DateTime.Now);

            if (slotToCheck == null)
            {
                throw new Exception("The current time has no valid slot.");
            }

            foreach (ObjVerEx item in activeScripts)
            {
                List<Lookup> medsToGive = new List<Lookup>();
                medsToGive.AddRange(searchService.GetMedsScheduledForTimeSlot(slotToCheck, item, true));

                //Add to meds dropdown
                foreach (Lookup med in medsToGive)
                {
                    env.ObjVerEx.AddLookup(Configuration.MedsOnScript, med.ToLatestObjVer(env.Vault));
                }
            }

            env.ObjVerEx.SaveProperties();
        }

        public void SetMedsToGive(EventHandlerEnvironment env)
        {
            //Get ScriptManagementObject
            ScriptManagementSearchService searchService = new ScriptManagementSearchService(env.Vault, Configuration);
            List<ObjVerEx> activeScripts = searchService.GetAllScriptManagementObject(env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID());

            //No object show error
            if (activeScripts.Count == 0)
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

            List<Lookup> medsToGive = new List<Lookup>();
            foreach (ObjVerEx script in activeScripts)
            {
                medsToGive.AddRange(searchService.GetMedsScheduledForTimeSlot(slotToCheck, script, false));
            }

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
