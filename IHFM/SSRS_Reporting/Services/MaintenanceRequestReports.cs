using SSRS_Reporting.SSRS_Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRS_Reporting.Services
{
    public class MaintenanceRequestReports:BaseReportService
    {
        public MaintenanceRequestReports() : base()
        {
            
        }

        public byte[] GetMaintenanceRequestReport(int objectId)
        {
            ParameterValue[] repParams = new ParameterValue[] { new ParameterValue { Name = "ObjectId", Value = objectId.ToString()} };
            return reportManager.Render("Reports/Maintenance", "Maintenance", "PDF", repParams); ;
        }
    }
}
