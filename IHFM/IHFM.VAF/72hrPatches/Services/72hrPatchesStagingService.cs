using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class _72hrPatchesStagingService
    {
        readonly DatabaseConnector _databaseConnector;
        Configuration _configuration;
        public _72hrPatchesStagingService(Configuration configuration)
        {
            _configuration = configuration;

            _databaseConnector = new DatabaseConnector(_configuration.SQLExport_Server, _configuration.SQLExport_Database);
        }

        public void StageFutureRecords(ObjVerEx patchRecord)
        {
            int residentId = patchRecord.GetLookupID(_configuration.Patches_Resident);
            int mddId = patchRecord.ObjID.ID; //patchRecord.GetLookupID(_configuration.Patches_Patch);

            string medsName = patchRecord.GetProperty(_configuration.Patches_Patch).GetValueAsLocalizedText();
            string timeslot = patchRecord.GetProperty(_configuration.Patches_Timeslot).GetValueAsLocalizedText();

            string endDate = patchRecord.GetProperty(_configuration.Patches_EndDate).GetValueAsLocalizedText();
            DateTime endDateTime = DateTime.Parse(endDate);
            DateTime recordDate = DateTime.Now.Date;

            do
            {
                //insert
                StoredProc storedProc = new StoredProc();
                storedProc.procedureName = "Insert72hrRecord";

                storedProc.storedProcParams = new Dictionary<string, object>();
                storedProc.storedProcParams.Add("@Resident_Id", residentId);
                storedProc.storedProcParams.Add("@Medicine_Dosage_Dispense_Id", mddId);
                storedProc.storedProcParams.Add("@MedsName", medsName);
                storedProc.storedProcParams.Add("@TimeSlot", timeslot);
                storedProc.storedProcParams.Add("@RecordDate", recordDate);

                _databaseConnector.ExecuteStoredProc(storedProc);

                recordDate = recordDate.AddHours(72);
            }
            while (DateTime.Compare(recordDate, endDateTime) < 0);
        }

        public void ClearFutureRecords(ObjVerEx patchRecord)
        {
            int residentId = patchRecord.GetLookupID(_configuration.Patches_Resident);
            int mddId = patchRecord.GetLookupID(_configuration.Patches_Patch);

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "ClearFuture72hrRecord";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@Resident_Id", residentId);
            storedProc.storedProcParams.Add("@Medicine_Dosage_Dispense_Id", mddId);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
    }
}
