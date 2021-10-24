using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;
namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.Resident")]
        public void SetRoomVacantWhenInactive(EventHandlerEnvironment env)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            bool active = env.ObjVerEx.GetProperty(Configuration.Active).GetValue<bool>();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(env.Vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(!active, currentRoomObjVerEx);
        }
    }
}
