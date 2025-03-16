using SSRS_Reporting.SSRS_Reports;

namespace SSRS_Reporting.Services
{
    public class MealBookingReports : BaseReportService
    {
        public byte[] GetMealBookingReport(
            string reportDate, 
            string reportInvoice, 
            string reportReference, 
            string reportName, 
            string reportSurname,
            string reportTelephone,
            string reportEmail,
            string MealItem1,
            string MealItem2,
            string MealItem3,
            string MealItem4,
            string MealItem5,
            string MealItem6,
            string discount)
        {
            ParameterValue[] repParams = new ParameterValue[] {
                new ParameterValue { Name = "ReportDate", Value = reportDate},
                new ParameterValue { Name = "ReportInvoice", Value = reportInvoice},
                new ParameterValue { Name = "ReportReference", Value = reportReference},
                new ParameterValue { Name = "ReportName", Value = reportName},
                new ParameterValue { Name = "ReportSurname", Value = reportSurname},
                new ParameterValue { Name = "ReportTelephone", Value = reportTelephone},
                new ParameterValue { Name = "ReportEmail", Value = reportEmail},

                new ParameterValue { Name = "MealItem1", Value = MealItem1},
                new ParameterValue { Name = "MealItem2", Value = MealItem2},
                new ParameterValue { Name = "MealItem3", Value = MealItem3},
                new ParameterValue { Name = "MealItem4", Value = MealItem4},
                new ParameterValue { Name = "MealItem5", Value = MealItem5},
                new ParameterValue { Name = "MealItem6", Value = MealItem6},
                new ParameterValue { Name = "Discount", Value = discount}
            };
            return reportManager.Render("Reports/Jaffa", "001_MealBooking_ProdTest", "PDF", repParams);
        }
    }
}
