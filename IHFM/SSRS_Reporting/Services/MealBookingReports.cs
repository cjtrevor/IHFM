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
            string reportCommentsNotes,
            string mealItem1,
            string mealItem2,
            string mealItem3,
            string mealItem4,
            string mealItem5,
            string mealItem6,
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
                new ParameterValue { Name = "ReportCommentsNotes", Value = reportCommentsNotes},
                new ParameterValue { Name = "MealItem1", Value = mealItem1},
                new ParameterValue { Name = "MealItem2", Value = mealItem2},
                new ParameterValue { Name = "MealItem3", Value = mealItem3},
                new ParameterValue { Name = "MealItem4", Value = mealItem4},
                new ParameterValue { Name = "MealItem5", Value = mealItem5},
                new ParameterValue { Name = "MealItem6", Value = mealItem6},
                new ParameterValue { Name = "Discount", Value = discount}
            };
            return reportManager.Render("Reports/Jaffa", "MealBooking_Invoice", "PDF", repParams);
        }
    }
}
