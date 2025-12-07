using SSRS_Reporting.SSRS_Reports;

namespace SSRS_Reporting.Services
{
    public class PanicButtonReports : BaseReportService
    {
        public PanicButtonReports() : base()
        {

        }

        public byte[] GetReport(string reportName, string jsonData)
        {
            ParameterValue[] repParams = new ParameterValue[] {
                new ParameterValue { Name = "JsonData", Value = jsonData}
            };
            return reportManager.Render("Reports/Trans50", reportName, "PDF", repParams);
        }
    }
}
