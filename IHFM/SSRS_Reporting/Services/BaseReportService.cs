using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRS_Reporting.Services
{
    public class BaseReportService
    {
        protected ReportManager reportManager;

        public BaseReportService()
        {
            reportManager = new ReportManager
                (
                    "http://mfiles:8080/ReportServer_MSSQLWEB/ReportExecution2005.asmx",
                    "IHFM Reports",
                    "IhfmReports!1",
                    "mfiles"
                );
        }
    }
}
