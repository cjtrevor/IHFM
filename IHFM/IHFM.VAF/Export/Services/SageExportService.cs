using IHFM.VAF.Export.Classes;
using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public struct SageExport
    {
        public SageTransactionHeader header;
        public List<SageTransactionItem> lineItems;
        public SageTransactionInvoice invoiceLine;
    }
    public class SageExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        DateTime exportStart;
        DateTime exportEnd;

        public SageExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
            exportEnd = DateTime.Now;
        }

        public void ExportBilling(string siteNumber, DateTime startDate, DateTime endDate)
        {  
            exportStart = startDate;
            exportEnd = endDate;

            ResidentSearchService searchService = new ResidentSearchService(_vault, _configuration);
            List<ObjVerEx> residents = searchService.GetAllResidentsForSite(siteNumber);

            List<SageExport> exports = new List<SageExport>();

            string expFile = $"C:\\IHFM\\SageExport\\Export_{siteNumber}_{startDate.ToString("yyMMdd")}-{endDate.ToString("yyMMdd")}";
            StreamWriter writer = new StreamWriter(expFile);

            writer.WriteLine(new SageTransactionHeader().GetHeaders());
            writer.WriteLine(new SageTransactionItem().GetHeaders());
            writer.WriteLine(new SageTransactionInvoice().GetHeaders());

            foreach (ObjVerEx res in residents)
            {
                SageExport exp = new SageExport
                {
                    header = GetTransactionHeader(res),
                    lineItems = GetTransactionItems(res)
                };

                exp.invoiceLine = GetTransactionInvoice(exp.lineItems);

                exports.Add(exp);
            }

            foreach (SageExport exp in exports)
            {
                writer.WriteLine(exp.header.GetDetails());

                exp.lineItems.ForEach(x => { writer.WriteLine(x.GetDetails()); });

                writer.WriteLine(exp.invoiceLine.GetDetails());
            }

            writer.Close();

        }

        private SageTransactionHeader GetTransactionHeader(ObjVerEx resident)
        {
            return new SageTransactionHeader
            {
                IDCUST = resident.GetPropertyText(_configuration.Resident_CPOARef),
                INVCDESC = $"Billing Period {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                IDACCTSET = "RES"
            };
        }

        private List<SageTransactionItem> GetTransactionItems(ObjVerEx resident)
        {
            List<SageTransactionItem> items = new List<SageTransactionItem>();

            //Tariffs
            double tariff = resident.GetProperty(_configuration.Resident_ActualAmountPayable).GetValue<double>();
            items.Add(new SageTransactionItem
            {
                CNTLINE = "20",
                IDDIST = "8012-295-02",
                TEXTDESC = $"BOARD & LODGING {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                AMTEXTN = tariff.ToString(),
                AMTTXBL = (tariff * 85 / 100).ToString(),
                TOTTAX = (tariff * 15 / 100).ToString()
            });

            //Ward Stock
            decimal wardStockCost = Decimal.Parse(GetWardStockCost(resident.ObjID.ID));

            if(wardStockCost > 0)
            { 
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "40",
                    IDDIST = "8446-295-02",
                    TEXTDESC = $"Ward Stock {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = wardStockCost.ToString(),
                    AMTTXBL = (wardStockCost * 85 / 100).ToString(),
                    TOTTAX = (wardStockCost * 15 / 100).ToString()
                });
            }

            //Time based care
            decimal tbcCost = Decimal.Parse(GetTimeBasedCareCost(resident.ObjID.ID,"ADL"));

            if(tbcCost > 0)
            { 
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "60",
                    IDDIST = "8446-295-02",
                    TEXTDESC = $"Time Based Care {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = tbcCost.ToString(),
                    AMTTXBL = (tbcCost * 85 / 100).ToString(),
                    TOTTAX = (tbcCost * 15 / 100).ToString()
                });
            }

            decimal tbcClinicCost = Decimal.Parse(GetTimeBasedCareCost(resident.ObjID.ID, "CLNC"));

            if (tbcClinicCost > 0)
            {
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "60",
                    IDDIST = "8446-295-02",
                    TEXTDESC = $"Time Based Care {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = tbcClinicCost.ToString(),
                    AMTTXBL = (tbcClinicCost * 85 / 100).ToString(),
                    TOTTAX = (tbcClinicCost * 15 / 100).ToString()
                });
            }

            return items;
        }

        private SageTransactionInvoice GetTransactionInvoice(List<SageTransactionItem> lines)
        {
            string amountDue = lines.Sum(x => Decimal.Parse(x.AMTEXTN)).ToString();
            return new SageTransactionInvoice
            {
                DATEDUE = DateTime.Now.ToShortDateString(),
                AMTDUE = amountDue,
                AMTDUEHC = amountDue
            };
        }

        private string GetWardStockCost(int residentId)
        {
            DatabaseConnector connector = new DatabaseConnector();

            StoredProc proc = new StoredProc
            {
                procedureName = "sp_GetWardStockRecordsForResidentPeriod",
                storedProcParams = new Dictionary<string, object>()
            };

            proc.storedProcParams.Add("@ResidentID", residentId);
            proc.storedProcParams.Add("@StartDate", exportStart);
            proc.storedProcParams.Add("@EndDate", exportEnd);

            return connector.ExecuteStoredProcScalar(proc);
        }

        private string GetTimeBasedCareCost(int residentId, string type)
        {
            DatabaseConnector connector = new DatabaseConnector();

            StoredProc proc = new StoredProc
            {
                procedureName = "sp_GetTimeBasedCareRecordsForResidentPeriod",
                storedProcParams = new Dictionary<string, object>()
            };

            proc.storedProcParams.Add("@ResidentID", residentId);
            proc.storedProcParams.Add("@StartDate", exportStart);
            proc.storedProcParams.Add("@EndDate", exportEnd);
            proc.storedProcParams.Add("@Type", type);

            return connector.ExecuteStoredProcScalar(proc);
        }
    }

   
}
