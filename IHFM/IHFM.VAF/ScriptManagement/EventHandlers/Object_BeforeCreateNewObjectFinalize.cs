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
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChanges, Class = "MFiles.Class.MedsGiven", Priority = 100)]
        public void AfterCheckInChangesMedsGiven(EventHandlerEnvironment env)
        {
            ExportMedsGiven(env, "CHR");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChanges, Class = "MFiles.Class.PrnMedsGiven", Priority = 100)]
        public void AfterCheckInChangesPrnMedsGiven(EventHandlerEnvironment env)
        {
            ExportMedsGiven(env, "PRN");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MedsGiven", Priority = 100)]
        public void BeforeCreateNewMedsGiven(EventHandlerEnvironment env)
        {
            ExportMedsGiven(env, "CHR");

            if(!env.ObjVerEx.HasValue(Configuration.MedsGiven_Adhoc) || !env.ObjVerEx.GetProperty(Configuration.MedsGiven_Adhoc).GetValue<bool>())
                SetMedsToGive(env);           
        }
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.PrnMedsGiven", Priority = 100)]
        public void BeforeCreateNewPRNMedsGiven(EventHandlerEnvironment env)
        {
            ExportMedsGiven(env, "PRN");

            if (!env.ObjVerEx.HasValue(Configuration.MedsGiven_Adhoc) || !env.ObjVerEx.GetProperty(Configuration.MedsGiven_Adhoc).GetValue<bool>())
                SetPRNMedsToGive(env);            
        }
        private void ExportMedsGiven(EventHandlerEnvironment env, string type)
        {
            ScriptControlExportService service = new ScriptControlExportService(env.Vault, Configuration);
            service.ExportMedsGiven(env.ObjVerEx, type);
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

            List<Lookup> medsToGive = new List<Lookup>();
            foreach (ObjVerEx script in activeScripts)
            {
                medsToGive.AddRange(searchService.GetMedsScheduledForTimeSlot(slotToCheck, script, false));
            }

            if (medsToGive.Count == 0)
            {
                throw new Exception("There is no medicine scheduled for the current time");
            }

            //Add to meds dropdown
            foreach  (Lookup med in medsToGive)
            {
                env.ObjVerEx.AddLookup(Configuration.MedsOnScript, med.ToLatestObjVer(env.Vault));
            }

            env.ObjVerEx.SaveProperties();
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.ScriptManagement", Priority = 100)]
        public void AfterCreateNewScriptManagement(EventHandlerEnvironment env)
        {
            
        }

       
    }
}
