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

        public byte[] GetMaintenanceRequestReport(string objectId, string resident, string staff, string jobAssignedTo, string jobToBeDone, 
            string jobDate, string timeStarted, string timeFinished, string comments)
        {
            ParameterValue[] repParams = new ParameterValue[] { 
                new ParameterValue { Name = "ObjectId", Value = objectId},
                new ParameterValue { Name = "Resident", Value = resident},
                new ParameterValue { Name = "Staff", Value = staff},
                new ParameterValue { Name = "JobAssignedTo", Value = jobAssignedTo},
                new ParameterValue { Name = "JobToBeDone", Value = jobToBeDone},
                new ParameterValue { Name = "JobDate", Value = jobDate},
                new ParameterValue { Name = "TimeStarted", Value = timeStarted},
                new ParameterValue { Name = "TimeFinished", Value = timeFinished},
                new ParameterValue { Name = "Comments", Value = comments}
            };
            return reportManager.Render("Reports/Trans50", "MaintReqPdf", "PDF", repParams);
        }
    }
}
