﻿using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class RoomSearchService
    {
        private Vault _vault;
        private Configuration _configuration;

        public RoomSearchService(Configuration configuration, Vault vault)
        {
            _configuration = configuration;
            _vault = vault;
        }

        public List<ObjVerEx> GetAllRooms()
        {
            MFSearchBuilder roomSearch = new MFSearchBuilder(_vault);
            roomSearch.ObjType(_configuration.Room_Object);
            roomSearch.Deleted(false);
            return roomSearch.FindEx();
        }

        public List<ObjVerEx> GetAllRoomsBySite(int siteId)
        {
            List<ObjVerEx> allRooms = GetAllRooms();
            return allRooms.Where(x => x.GetLookupID(_configuration.BaseSiteID) == siteId).ToList();
        }

        public List<ObjVerEx> GetRoomsByZone(int siteId, List<int> zoneIds)
        {
            List<ObjVerEx> allRooms = GetAllRoomsBySite(siteId);

            return allRooms;//.Where(x => zoneIds.Contains(x.GetLookupID(_configuration.Room_Zone))).ToList();
        }

        public ObjVerEx GetRoomBySiteAndNumber(int siteId, string number)
        {
            MFSearchBuilder roomSearch = new MFSearchBuilder(_vault);
            roomSearch.ObjType(_configuration.Room_Object);
            roomSearch.Property(_configuration.BaseSite, MFDataType.MFDatatypeLookup, siteId);
            roomSearch.Property(_configuration.Room_RoomNumber, MFDataType.MFDatatypeText, number);
            roomSearch.Deleted(false);

            List<ObjVerEx> results = roomSearch.FindEx();
            if (results.Count > 0)
                return results.FirstOrDefault();

            return null;
        }

        public List<ObjVerEx> GetRoomsBySiteAndNumber(int siteId, string number)
        {
            MFSearchBuilder roomSearch = new MFSearchBuilder(_vault);
            roomSearch.ObjType(_configuration.Room_Object);
            roomSearch.Property(_configuration.BaseSite, MFDataType.MFDatatypeLookup, siteId);
            roomSearch.Property(_configuration.Room_RoomNumber, MFDataType.MFDatatypeText, number);
            roomSearch.Deleted(false);

            return roomSearch.FindEx();
        }
    }
}
