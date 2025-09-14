using SSRS_Reporting.SSRS_Reports;
using System.Net;

namespace SSRS_Reporting
{
    public class ReportManager
    {
        private readonly ReportExecutionService _reportServerExecutionService;

        public ReportManager
            (
            string reportServerWsdlUrl,
            string username,
            string password,
            string domain)
        {
            _reportServerExecutionService = new ReportExecutionService
            {
                Url = reportServerWsdlUrl,
                Credentials = new NetworkCredential(username, password, domain)
            };
        }

        public byte[] Render
            (
            string reportDirectory,
            string reportName,
            string reportFormat,
            ParameterValue[] parameters
            )
        {
            _reportServerExecutionService.ExecutionHeaderValue = new ExecutionHeader();
            ExecutionInfo execInfo = _reportServerExecutionService.LoadReport($"/{reportDirectory}/{reportName}", null);
            _reportServerExecutionService.SetExecutionParameters(parameters, "en-us");

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings;
            string[] streamIds;

            var result = _reportServerExecutionService.Render(reportFormat, null, out extension, out encoding, out mimeType, out warnings, out streamIds);

            return result;
        }


    }
}
