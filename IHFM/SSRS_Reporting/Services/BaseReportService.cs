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
                    "http://t50cloud:8099/ReportServer/ReportExecution2005.asmx",
                    "Reporting",
                    "R3porting1!",
                    "T50Cloud"
                );
        }
    }
}
