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
            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            if(env.ObjVerEx.HasValue(Configuration.DailyCare_IsComplete) && changes.HasChanged(Configuration.DailyCare_IsComplete) 
                && env.ObjVerEx.GetProperty(Configuration.DailyCare_IsComplete).GetValue<bool>())
            { 
                //UpdateResidentBathCount(env.Vault, env.ObjVerEx);
                //UpdateResidentBowelCount(env.Vault, env.ObjVerEx);
                //UpdateResidentEatCount(env.Vault, env.ObjVerEx);
            }
        }

        private void UpdateResidentBowelCount(Vault vault, ObjVerEx dailyCare)
        {
            Lookup resLookup = dailyCare.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(vault, resLookup);
            ResidentPropertyService propServ = new ResidentPropertyService(vault, Configuration);

            if (resident.GetLookupID(Configuration.Room_Zone) != Configuration.Zone_Independant.ID)
                propServ.SetNoBowelMovementCount(resident, dailyCare.HasValue(Configuration.DailyCare_BowelMovement));
        }

        private void UpdateResidentBathCount(Vault vault, ObjVerEx dailyCare)
        {
            Lookup resLookup = dailyCare.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(vault, resLookup);
            ResidentPropertyService propServ = new ResidentPropertyService(vault, Configuration);
            if (resident.GetLookupID(Configuration.Room_Zone) != Configuration.Zone_Independant.ID)
               propServ.SetNoBathCount(resident, dailyCare.HasValue(Configuration.DailyCare_BathType));
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
