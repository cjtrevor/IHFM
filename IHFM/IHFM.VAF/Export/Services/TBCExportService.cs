using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Common;

namespace IHFM.VAF
{
    public class TBCExportService
    {
        private Configuration _configuration;
        public TBCExportService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public enum TbcType
        {
            TBC,
            Clinic
        }
        public void ExportRecord(ObjVerEx tbc, TbcType tbcType)
        {
            switch (tbcType)
            {
                case TbcType.TBC:
                    ExportTBCRecord(tbc,"ADL");
                    break;
                case TbcType.Clinic:
                    ExportTBCRecord(tbc,"CLNC");
                    break;
            }
        }
        public void ExportTBCRecord(ObjVerEx tbc, string type)
        {
            int objectId = tbc.ObjID.ID;
            int siteId = Int32.Parse(tbc.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());

            string startDate = tbc.GetProperty(_configuration.StartTimeTBC).GetValueAsLocalizedText();
            string monthName = DateTime.Parse(startDate).ToString("MMMM", CultureInfo.InvariantCulture);
            int year = DateTime.Parse(startDate).Year;


            string costForService = tbc.GetPropertyText(_configuration.CostForService);
            decimal cost;

            if(!Decimal.TryParse(costForService, out cost))
            {
                cost = 0.0m;
            }

            DatabaseConnector connector = new DatabaseConnector();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportTBCRecord";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@ObjectID", objectId);
            storedProc.storedProcParams.Add("@SiteId", siteId);
            storedProc.storedProcParams.Add("@Month", monthName);
            storedProc.storedProcParams.Add("@Year", year);
            storedProc.storedProcParams.Add("@Cost", cost);
            storedProc.storedProcParams.Add("@TBCType",type);

            connector.ExecuteStoredProc(storedProc);
        }
    }
}
