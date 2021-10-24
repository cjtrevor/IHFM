using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public class RoomPropertyService
    {
        private Configuration _configuration;
        public RoomPropertyService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void SetRoomVacantStatus(bool Vacant, ObjVerEx room)
        {
            room.SaveProperty(_configuration.Vacant, MFDataType.MFDatatypeBoolean, Vacant);
        }
    }
}
