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

        public void UpdateRoomResidentTariff(ObjVerEx room, int tariff, Vault vault)
        {
            ResidentSearchService search = new ResidentSearchService(vault, _configuration);
            List<ObjVerEx> roomResidents = search.GetResidentByRoom(room.ID);
            ObjVerEx resident = new ObjVerEx();

            bool roomHasResident = false;
            foreach (ObjVerEx res in roomResidents)
            {
                if (res.HasValue(_configuration.Active) && res.GetProperty(_configuration.Active).GetValue<bool>())
                {
                    resident = res;
                    roomHasResident = true;
                    break;
                }
            }

            if (roomHasResident)
            {
                ResidentPropertyService serv = new ResidentPropertyService(vault, _configuration);
                serv.SetTariff(resident, tariff);
            }
        }
    }
}
