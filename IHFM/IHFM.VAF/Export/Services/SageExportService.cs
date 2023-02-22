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

        public void ExportBilling(int siteId, string siteName, DateTime startDate, DateTime endDate)
        {  
            exportStart = startDate;
            exportEnd = endDate;

            ResidentSearchService searchService = new ResidentSearchService(_vault, _configuration);
            List<ObjVerEx> residents = searchService.GetAllResidentsForSite(siteId);

            List<SageExport> exports = new List<SageExport>();

            string expFile = $"C:\\IHFM\\SageExport\\Export_{siteName}_{startDate.ToString("yyMMdd")}-{endDate.ToString("yyMMdd")}_{DateTime.Now.Ticks}.csv";
            StreamWriter writer = new StreamWriter(expFile);

            writer.WriteLine(new SageTransactionHeader().GetHeaders());
            writer.WriteLine(new SageTransactionItem().GetHeaders());
            writer.WriteLine(new SageTransactionInvoice().GetHeaders());

            int itemNumber = 1;
            foreach (ObjVerEx res in residents)
            {
                SageExport exp = new SageExport
                {
                    header = GetTransactionHeader(res, itemNumber),
                    lineItems = GetTransactionItems(res, itemNumber)
                };

                exp.invoiceLine = GetTransactionInvoice(exp.lineItems, itemNumber);

                exports.Add(exp);

                itemNumber++;
            }

            foreach (SageExport exp in exports)
            {
                writer.WriteLine(exp.header.GetDetails());

                exp.lineItems.ForEach(x => { writer.WriteLine(x.GetDetails()); });

                writer.WriteLine(exp.invoiceLine.GetDetails());
            }

            writer.Close();

        }

        private SageTransactionHeader GetTransactionHeader(ObjVerEx resident, int itemNumber)
        {
            return new SageTransactionHeader
            {
                IDCUST = resident.GetPropertyText(_configuration.Resident_CPOARef),
                CNTITEM = itemNumber.ToString(),
                INVCDESC = $"Billing Period {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                IDACCTSET = "RES"
            };
        }

        private List<SageTransactionItem> GetTransactionItems(ObjVerEx resident, int itemNumber)
        {
            List<SageTransactionItem> items = new List<SageTransactionItem>();

            //Tariffs
            bool hasValidTariff = resident.HasValue(_configuration.Resident_ActualAmountPayable);
            double tariff = hasValidTariff ? resident.GetProperty(_configuration.Resident_ActualAmountPayable).GetValue<double>() : 0;
            items.Add(new SageTransactionItem
            {
                CNTLINE = "20",
                CNTITEM = itemNumber.ToString(),
                IDACCTREV = "8012-295-02",
                TEXTDESC = $"BOARD & LODGING {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                AMTEXTN = tariff.ToString("0.00"),
                AMTTXBL = (tariff * 85 / 100).ToString("0.00"),
                BASETAX1 = (tariff * 85 / 100).ToString("0.00"),
                TOTTAX = (tariff * 15 / 100).ToString("0.00")
            }) ;

            //Ward Stock
            decimal wardStockCost = Decimal.Parse(GetWardStockCost(resident.ObjID.ID));

            if(wardStockCost > 0)
            { 
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "40",
                    CNTITEM = itemNumber.ToString(),
                    IDACCTREV = "8446-295-02",
                    TEXTDESC = $"Ward Stock {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = wardStockCost.ToString("0.00"),
                    AMTTXBL = (wardStockCost * 85 / 100).ToString("0.00"),
                    BASETAX1 = (wardStockCost * 85 / 100).ToString("0.00"),
                    TOTTAX = (wardStockCost * 15 / 100).ToString("0.00")
                });
            }

            //Time based care
            decimal tbcCost = Decimal.Parse(GetTimeBasedCareCost(resident.ObjID.ID,"ADL"));

            if(tbcCost > 0)
            { 
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "60",
                    CNTITEM = itemNumber.ToString(),
                    IDACCTREV = "8446-295-02",
                    TEXTDESC = $"Time Based Care {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = tbcCost.ToString("0.00"),
                    AMTTXBL = (tbcCost * 85 / 100).ToString("0.00"),
                    BASETAX1 = (tbcCost * 85 / 100).ToString("0.00"),
                    TOTTAX = (tbcCost * 15 / 100).ToString("0.00")
                });
            }

            decimal tbcClinicCost = Decimal.Parse(GetTimeBasedCareCost(resident.ObjID.ID, "CLNC"));

            if (tbcClinicCost > 0)
            {
                items.Add(new SageTransactionItem
                {
                    CNTLINE = "60",
                    CNTITEM = itemNumber.ToString(),
                    IDACCTREV = "8446-295-02",
                    TEXTDESC = $"Time Based Care (Clinic) {exportStart.ToShortDateString()} - {exportEnd.ToShortDateString()}",
                    AMTEXTN = tbcClinicCost.ToString("0.00"),
                    AMTTXBL = (tbcClinicCost * 85 / 100).ToString("0.00"),
                    BASETAX1 = (tbcClinicCost * 85 / 100).ToString("0.00"),
                    TOTTAX = (tbcClinicCost * 15 / 100).ToString("0.00")
                });
            }

            return items;
        }

        private SageTransactionInvoice GetTransactionInvoice(List<SageTransactionItem> lines, int itemNumber)
        {
            string amountDue = lines.Sum(x => Decimal.Parse(x.AMTEXTN)).ToString("0.00");
            return new SageTransactionInvoice
            {
                CNTITEM = itemNumber.ToString(),
                AMTDUE = amountDue,
                AMTDUEHC = amountDue
            };
        }

        private string GetWardStockCost(int residentId)
        {
            DatabaseConnector connector = new DatabaseConnector(_configuration.SQLExport_Server, _configuration.SQLExport_Database);

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
            DatabaseConnector connector = new DatabaseConnector(_configuration.SQLExport_Server, _configuration.SQLExport_Database);

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
