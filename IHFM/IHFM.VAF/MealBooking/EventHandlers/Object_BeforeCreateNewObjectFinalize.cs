using MFilesAPI;
using MFiles.VAF.Common;
using System;
using SSRS_Reporting.Services;
using System.IO;
using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.MealBooking")]
        public void BeforeNewMealBookingCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVerEx.ID.ToString();
            string objectVersion = env.ObjVer.Version.ToString();

            var filePath = $"C:\\ReportingConfigurator\\SSRS Temp Output\\{objectId}_{objectVersion}.pdf";

            var mealItem1 = MealItemReportParameter(env, Configuration.MealBooking_MealItem1, Configuration.MealBooking_Qty1);
            var mealItem2 = MealItemReportParameter(env, Configuration.MealBooking_MealItem2, Configuration.MealBooking_Qty2);
            var mealItem3 = MealItemReportParameter(env, Configuration.MealBooking_MealItem3, Configuration.MealBooking_Qty3);
            var mealItem4 = MealItemReportParameter(env, Configuration.MealBooking_MealItem4, Configuration.MealBooking_Qty4);
            var mealItem5 = MealItemReportParameter(env, Configuration.MealBooking_MealItem5, Configuration.MealBooking_Qty5);
            var mealItem6 = MealItemReportParameter(env, Configuration.MealBooking_MealItem6, Configuration.MealBooking_Qty6);

            var residentOrOtherLookupId = env.ObjVerEx.GetLookupID(Configuration.MealBooking_ResidentOrOther);

            var reportParam_Name = "";
            var reportParam_Surname = "";
            var reportParam_Telephone = "";

            //RESIDENT = 1, OTHER = 2
            switch (residentOrOtherLookupId)
            {
                case 1:
                    Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.MealBooking_Resident).TypedValue.GetValueAsLookup();
                    ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

                    if (resident == null)
                        return;

                    reportParam_Name = resident.GetProperty(Configuration.Resident_FirstName).GetValueAsLocalizedText();
                    reportParam_Surname = resident.GetProperty(Configuration.Resident_Surname).GetValueAsLocalizedText();
                    reportParam_Telephone = resident.GetProperty(Configuration.Resident_CellNumber).GetValueAsLocalizedText();

                    break;
                case 2:
                    reportParam_Name = env.ObjVerEx.GetProperty(Configuration.MealBooking_Name).GetValueAsLocalizedText();
                    reportParam_Surname = env.ObjVerEx.GetProperty(Configuration.MealBooking_Surname).GetValueAsLocalizedText();
                    reportParam_Telephone = env.ObjVerEx.GetProperty(Configuration.MealBooking_Telephone).GetValueAsLocalizedText();
                    break;
                default:
                    return;
            }

            var discount = env.ObjVerEx.GetProperty(Configuration.MealBooking_Discount).GetValueAsLocalizedText();
            var reportParam_Discount = string.IsNullOrEmpty(discount) ? "0" : discount;

            var reportParam_Date = env.ObjVerEx.GetProperty(Configuration.MealBooking_Date).GetValueAsLocalizedText();
            var reportParam_Invoice = objectId;
            var reportParam_Reference = env.ObjVerEx.GetProperty(Configuration.MealBooking_Reference).GetValueAsLocalizedText();
            var reportParam_Email = env.ObjVerEx.GetProperty(Configuration.MealBooking_EmailAddress).GetValueAsLocalizedText();
            var reportParam_CommentsNotes = env.ObjVerEx.GetProperty(Configuration.MealBooking_CommentsNotes).GetValueAsLocalizedText();

            MealBookingReports mealBookingReports = new MealBookingReports();

            var reportTestDynamicParams = mealBookingReports.GetMealBookingReport(
                reportParam_Date,
                reportParam_Invoice,
                reportParam_Reference,
                reportParam_Name,
                reportParam_Surname,
                reportParam_Telephone,
                reportParam_Email,
                reportParam_CommentsNotes,
                mealItem1,
                mealItem2,
                mealItem3,
                mealItem4,
                mealItem5,
                mealItem6,
                reportParam_Discount);

            File.WriteAllBytes(filePath, reportTestDynamicParams);

            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MB_{objectId}-{objectVersion}", "pdf", filePath);

            File.Delete(filePath);
        }

        private string MealItemReportParameter(EventHandlerEnvironment env, MFIdentifier mealItem, MFIdentifier mealBookingItemQty)
        {
            var mealBooking_MealItemProperty = env.ObjVerEx.GetProperty(mealItem);

            if (mealBooking_MealItemProperty == null)
                return "||0||0";

            var mealBooking_MealItemLookup = mealBooking_MealItemProperty.TypedValue.GetValueAsLookup();

            if (mealBooking_MealItemLookup == null)
                return "||0||0";

            ObjVerEx mealItemObject = new ObjVerEx(env.Vault, mealBooking_MealItemLookup);

            var mealBooking_MealItemQtyProperty = env.ObjVerEx.GetProperty(mealBookingItemQty);

            var mealBooking_MealItemValue = mealBooking_MealItemProperty.GetValueAsLocalizedText();
            var mealItem_PriceValue = mealItemObject.GetPropertyText(Configuration.FoodItem_Price).Replace(',', '.');
            var mealBooking_MealItemQtyValue = mealBooking_MealItemQtyProperty.GetValueAsLocalizedText().Replace(',', '.');

            return mealBooking_MealItemValue + "||" + mealBooking_MealItemQtyValue + "||" + mealItem_PriceValue;
        }

    }
}
