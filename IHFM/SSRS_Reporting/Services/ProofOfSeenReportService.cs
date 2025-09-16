using SSRS_Reporting.SSRS_Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRS_Reporting.Services
{
    public class ProofOfSeenReportService : BaseReportService
    {
        public ProofOfSeenReportService() : base()
        {

        }

        public byte[] GetReport(string reportName, string jsonData)
        {
            ParameterValue[] repParams = new ParameterValue[] {
                new ParameterValue { Name = "JsonData", Value = jsonData}
            };
            return reportManager.Render("Reports", reportName, "PDF", repParams);
        }

    }
}
