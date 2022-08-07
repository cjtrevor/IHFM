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
            if (HasDuplicateResident(env.ObjVerEx, env.Vault))
                throw new Exception("A resident with this ID number already exists. All ID numbers should be unique.");

            SetRoomNotVacant(env.ObjVerEx, env.Vault);
        }

        public void SetRoomNotVacant(ObjVerEx resident, Vault vault)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = resident.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(false, currentRoomObjVerEx);
        }

        public bool HasDuplicateResident(ObjVerEx resident, Vault vault)
        {
            ResidentSearchService resSearch = new ResidentSearchService(vault, Configuration);
            string IdNumber = resident.GetProperty(Configuration.IDNumber).GetValueAsLocalizedText();

            return resSearch.GetResidentByIDNumber(IdNumber) != null;
        }

    }
}
