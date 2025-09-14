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
                    "http://singh/ReportServer/ReportExecution2005.asmx",
                    "username",
                    "password",
                    "domain"
                );
        }
    }
}











