using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.Resident")]
        public void BeforeCreatingANewResident(EventHandlerEnvironment env)
        {
            CheckDuplicateResident(env.ObjVerEx, env.Vault);

            SetRoomNotVacant(env.ObjVerEx, env.Vault);
        }

        public void SetRoomNotVacant(ObjVerEx resident, Vault vault)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = resident.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(false, currentRoomObjVerEx);
        }

        public void CheckDuplicateResident(ObjVerEx resident, Vault vault)
        {
            ResidentSearchService resSearch = new ResidentSearchService(vault, Configuration);
            string IdNumber = resident.GetProperty(Configuration.IDNumber).GetValueAsLocalizedText();

            List<ObjVerEx> residents = resSearch.GetResidentsByIDNumber(IdNumber);

            foreach(ObjVerEx res in residents)
            {
                if(res.ID != resident.ID)
                {
                    string resSite = res.GetProperty(Configuration.BaseSite).GetValueAsLocalizedText();
                    string residentName = res.GetProperty(Configuration.Resident_ResidentDetail).GetValueAsLocalizedText();
                    string active = res.HasValue(Configuration.Active) && res.GetProperty(Configuration.Active).GetValue<bool>() ? "active" : "inactive";

                    throw new Exception($"A resident with this ID number already exists at {resSite}. Name: {residentName} Status: {active}");                
                }
            }
        }

    }
}
