using SSRS_Reporting.SSRS_Reports;

namespace SSRS_Reporting.Services
{
    public class HealthcareFormReports : BaseReportService
    {
        public HealthcareFormReports() : base()
        {

        }

        public byte[] GetReport(string reportName, string jsonData)
        {
            ParameterValue[] repParams = new ParameterValue[] {
                new ParameterValue { Name = "JsonData", Value = jsonData}
            };
            return reportManager.Render("Reports/Trans50/Sites", reportName, "PDF", repParams);
        }
    }
}
