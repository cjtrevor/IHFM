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
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.DailyCare")]
        public void AfterDailyCareCheckInChangesFinalized(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log($"AfterDailyCareCheckInChangesFinalized START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            DailyCareLogger.Log("AfterDailyCareCheckInChangesFinalized — checking IsComplete property");
            if(env.ObjVerEx.HasValue(Configuration.DailyCare_IsComplete) && changes.HasChanged(Configuration.DailyCare_IsComplete) 
                && env.ObjVerEx.GetProperty(Configuration.DailyCare_IsComplete).GetValue<bool>())
            { 
                DailyCareLogger.Log("AfterDailyCareCheckInChangesFinalized — IsComplete changed to true");
                //UpdateResidentBathCount(env.Vault, env.ObjVerEx);
                //UpdateResidentBowelCount(env.Vault, env.ObjVerEx);
                //UpdateResidentEatCount(env.Vault, env.ObjVerEx);
            }

            sw.Stop();
            DailyCareLogger.Log($"AfterDailyCareCheckInChangesFinalized END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateResidentBowelCount(Vault vault, ObjVerEx dailyCare)
        {
            DailyCareLogger.Log("UpdateResidentBowelCount START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            Lookup resLookup = dailyCare.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(vault, resLookup);
            ResidentPropertyService propServ = new ResidentPropertyService(vault, Configuration);

            DailyCareLogger.Log("UpdateResidentBowelCount — checking zone");
            if (resident.GetLookupID(Configuration.Room_Zone) != Configuration.Zone_Independant.ID)
            {
                DailyCareLogger.Log("UpdateResidentBowelCount — calling SetNoBowelMovementCount");
                propServ.SetNoBowelMovementCount(resident, dailyCare.HasValue(Configuration.DailyCare_BowelMovement));
            }

            sw.Stop();
            DailyCareLogger.Log($"UpdateResidentBowelCount END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateResidentBathCount(Vault vault, ObjVerEx dailyCare)
        {
            DailyCareLogger.Log("UpdateResidentBathCount START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            Lookup resLookup = dailyCare.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(vault, resLookup);
            ResidentPropertyService propServ = new ResidentPropertyService(vault, Configuration);

            DailyCareLogger.Log("UpdateResidentBathCount — checking zone");
            if (resident.GetLookupID(Configuration.Room_Zone) != Configuration.Zone_Independant.ID)
            {
                DailyCareLogger.Log("UpdateResidentBathCount — calling SetNoBathCount");
                propServ.SetNoBathCount(resident, dailyCare.HasValue(Configuration.DailyCare_BathType));
            }

            sw.Stop();
            DailyCareLogger.Log($"UpdateResidentBathCount END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        //private void UpdateResidentEatCount(Vault vault, ObjVerEx dailyCare)
        //{
        //    Lookup resLookup = dailyCare.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
        //    ObjVerEx resident = new ObjVerEx(vault, resLookup);
        //    ResidentPropertyService propServ = new ResidentPropertyService(vault, Configuration);

        //    bool hadBreakfast = dailyCare.HasValue(Configuration.DailyCare_HadBreakfast) && dailyCare.GetProperty(Configuration.DailyCare_HadBreakfast).GetValue<bool>();
        //    bool hadLunch = dailyCare.HasValue(Configuration.DailyCare_HadLunch) && dailyCare.GetProperty(Configuration.DailyCare_HadLunch).GetValue<bool>();
        //    bool hadSupper = dailyCare.HasValue(Configuration.DailyCare_HadSupper) && dailyCare.GetProperty(Configuration.DailyCare_HadSupper).GetValue<bool>();

        //    propServ.SetNoEatCount(resident, !hadBreakfast && !hadLunch && !hadSupper);
        //}
    }
}
