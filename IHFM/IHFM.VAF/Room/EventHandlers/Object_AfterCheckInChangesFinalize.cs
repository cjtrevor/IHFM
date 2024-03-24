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
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.Room")]
        public void AftermakingChangesToARoom(EventHandlerEnvironment env)
        {
            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            foreach (PropertyValueChange change in changes.Changed)
            {
                if (change.PropertyDef == Configuration.RoomTariff.ID && change.ChangeType == PropertyValueChangeType.Modified)
                {
                    int itemId = env.ObjVerEx.GetLookupID(Configuration.RoomTariff);

                    RoomPropertyService service = new RoomPropertyService(Configuration);
                    service.UpdateRoomResidentTariff(env.ObjVerEx, itemId, env.Vault);
                    break;
                }
            }
        }
    }
}
