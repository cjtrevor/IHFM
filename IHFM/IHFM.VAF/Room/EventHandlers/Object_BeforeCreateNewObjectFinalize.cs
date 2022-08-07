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
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.Room")]
        public void BeforeCreatingANewRoom(EventHandlerEnvironment env)
        {
            if(HasDuplicateRoom(env.ObjVerEx,env.Vault))
            {
                throw new Exception("A room with the same nuber for the selected site already exists. Room numbers should be unique for each site.");
            }
        }

        public bool HasDuplicateRoom(ObjVerEx room, Vault vault)
        {
            RoomSearchService roomSearch = new RoomSearchService(Configuration, vault);
            string roomNumber = room.GetProperty(Configuration.Room_RoomNumber).GetValueAsLocalizedText();
            int siteId = room.GetLookupID(Configuration.BaseSite);

            return roomSearch.GetRoomBySiteAndNumber(siteId, roomNumber) != null; 
        }
    }
}
