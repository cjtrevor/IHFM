using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.Resident")]
        public void SetRoomNotVacant(EventHandlerEnvironment env)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(env.Vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(false, currentRoomObjVerEx);
        }

    }
}
