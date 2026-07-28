using IHFM.VAF.Email.Services;
using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ShiftAllocation")]
        public void BeforeCreateNewShiftAllocation(EventHandlerEnvironment env)
        {
            int totalTimeInMinutes = 60;

            var server_Start_Timestamp = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_StartDateTime).TypedValue.GetValueAsTimestamp();
            var local_Start_DateTime = server_Start_Timestamp.ToLocalDateTime();

            var local_End_DateTime = local_Start_DateTime.AddMinutes(totalTimeInMinutes);
            var server_End_Timestamp = local_End_DateTime.ToUtcTimestamp();

            Lookups staffAttendingLookups = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_StaffAttending).TypedValue.GetValueAsLookups();

            List<string> conflictMessages = new List<string>();

            foreach (Lookup staffLookup in staffAttendingLookups)
            {
                ShiftAllocationSearchService shiftAllocationSearchService = new ShiftAllocationSearchService(env.Vault, Configuration);
                var existingAllocations = shiftAllocationSearchService.SearchForExistingStaffShiftAllocations(
                    staffLookup.Item,
                    local_Start_DateTime,
                    env.ObjVer.ID
                );

                foreach (var existingAllocation in existingAllocations)
                {
                    var server_existingStart_Timestamp = existingAllocation.GetProperty(Configuration.ShiftAllocation_StartDateTime).TypedValue.GetValueAsTimestamp();
                    var server_existingEnd_Timestamp = existingAllocation.GetProperty(Configuration.ShiftAllocation_EndDateTime).TypedValue.GetValueAsTimestamp();

                    var local_existingStart = server_existingStart_Timestamp.ToLocalDateTime();
                    var local_existingEnd = server_existingEnd_Timestamp.ToLocalDateTime();

                    bool hasOverlap = local_Start_DateTime < local_existingEnd && local_existingStart < local_End_DateTime;

                    if (hasOverlap)
                    {
                        string staffName = staffLookup.DisplayValue;
                        string conflictMsg = string.Format(
                            "CONFLICT: {0}\n" +
                            "  Existing Shift: {1} at {2} - {3}\n" +
                            "  New Shift:      {4} at {5} - {6}\n",
                            staffName,
                            local_existingStart.ToString("dd MMM yyyy"),
                            local_existingStart.ToString("HH:mm"),
                            local_existingEnd.ToString("HH:mm"),
                            local_Start_DateTime.ToString("dd MMM yyyy"),
                            local_Start_DateTime.ToString("HH:mm"),
                            local_End_DateTime.ToString("HH:mm")
                        );
                        conflictMessages.Add(conflictMsg);
                    }
                }
            }

            if (conflictMessages.Count > 0)
            {
                string fullMessage = "Cannot create shift allocation - scheduling conflicts detected:\n\n" + string.Join("\n", conflictMessages) + "\n";
                throw new Exception(fullMessage);
            }

            var ReserveTimeslot = env.ObjVerEx.GetPropertyAsBoolean(Configuration.ShiftAllocation_ReserveTimeslot) ?? false;
            if (ReserveTimeslot)
            {
                SiteStockUpdateService siteStockUpdateService = new SiteStockUpdateService(env.Vault, Configuration);
                Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_Resident).TypedValue.GetValueAsLookup();
                ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);
                var siteIdFromResident = resident.GetLookupID(Configuration.BaseSiteID);

                UpdateSiteStockForShiftAllocation(env, siteIdFromResident, siteStockUpdateService, Configuration.ShiftAllocation_Item1HBC, Configuration.ShiftAllocation_Qty1HBC);
                UpdateSiteStockForShiftAllocation(env, siteIdFromResident, siteStockUpdateService, Configuration.ShiftAllocation_Item2HBC, Configuration.ShiftAllocation_Qty2HBC);
                UpdateSiteStockForShiftAllocation(env, siteIdFromResident, siteStockUpdateService, Configuration.ShiftAllocation_Item3HBC, Configuration.ShiftAllocation_Qty3HBC);
                UpdateSiteStockForShiftAllocation(env, siteIdFromResident, siteStockUpdateService, Configuration.ShiftAllocation_Item4HBC, Configuration.ShiftAllocation_Qty4HBC);
                UpdateSiteStockForShiftAllocation(env, siteIdFromResident, siteStockUpdateService, Configuration.ShiftAllocation_Item5HBC, Configuration.ShiftAllocation_Qty5HBC);
            }

            env.ObjVerEx.SetProperty(Configuration.ShiftAllocation_EndDateTime, MFDataType.MFDatatypeTimestamp, server_End_Timestamp);
            env.ObjVerEx.SaveProperties();
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.ShiftAllocation")]
        public void AfterCreateNewShiftAllocation(EventHandlerEnvironment env)
        {
            try
            {
                var server_Start_Timestamp = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_StartDateTime).TypedValue.GetValueAsTimestamp();
                var local_Start_DateTime = server_Start_Timestamp.ToLocalDateTime();

                var server_End_Timestamp = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_EndDateTime).TypedValue.GetValueAsTimestamp();
                var local_End_DateTime = server_End_Timestamp.ToLocalDateTime();

                Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_Resident).TypedValue.GetValueAsLookup();
                Lookups staffAttendingLookups = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_StaffAttending).TypedValue.GetValueAsLookups();

                TimeSpan duration = local_End_DateTime - local_Start_DateTime;
                int totalTimeInMinutes = (int)duration.TotalMinutes;

                List<string> calendarCategories = new List<string>();
                string staffMembers = "";
                var staffEmailAddresses = new List<string>();
                staffEmailAddresses.AddRange(Configuration.ShiftAllocation_MailLising.Split(';'));

                foreach (Lookup staffLookup in staffAttendingLookups)
                {
                    var staffObjVer = new ObjVerEx(env.Vault, staffLookup);
                    var emailAddress = staffObjVer.GetPropertyText(Configuration.Staff_EmailAddress);
                    

                    if (staffObjVer.HasProperty(Configuration.ShiftAllocation_CalendarCategory) && staffObjVer.HasValue(Configuration.ShiftAllocation_CalendarCategory))
                    {
                        var staffCalendarCategory = staffObjVer.GetProperty(Configuration.ShiftAllocation_CalendarCategory).TypedValue.GetValueAsLookup();

                        if (staffCalendarCategory != null)
                            calendarCategories.Add(staffCalendarCategory.DisplayValue);
                    }

                    staffMembers += $"{staffLookup.DisplayValue}\n";

                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        staffEmailAddresses.Add(emailAddress);
                    }
                }

                if (staffEmailAddresses.Count > 0)
                {
                    EmailService emailService = new EmailService(Configuration);

                    var residentObjVer = new ObjVerEx(env.Vault, residentLookup);

                    string residentName = residentLookup.DisplayValue;
                    string residentAddress = residentObjVer.GetPropertyText(Configuration.Resident_HomeAddress);
                    string subject = $"Shift Allocation - {residentName}";
                    string location = ""; // Get location from resident or site????

                    string item1Name = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Item1HBC);
                    string item2Name = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Item2HBC);
                    string item3Name = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Item3HBC);
                    string item4Name = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Item4HBC);
                    string item5Name = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Item5HBC);

                    var items = new List<string>();

                    if (!string.IsNullOrWhiteSpace(item1Name))
                        items.Add($"{item1Name} x {env.ObjVerEx.GetPropertyAsDouble(Configuration.ShiftAllocation_Qty1HBC) ?? 0}");

                    if (!string.IsNullOrWhiteSpace(item2Name))
                        items.Add($"{item2Name} x {env.ObjVerEx.GetPropertyAsDouble(Configuration.ShiftAllocation_Qty2HBC) ?? 0}");

                    if (!string.IsNullOrWhiteSpace(item3Name))
                        items.Add($"{item3Name} x {env.ObjVerEx.GetPropertyAsDouble(Configuration.ShiftAllocation_Qty3HBC) ?? 0}");

                    if (!string.IsNullOrWhiteSpace(item4Name))
                        items.Add($"{item4Name} x {env.ObjVerEx.GetPropertyAsDouble(Configuration.ShiftAllocation_Qty4HBC) ?? 0}");

                    if (!string.IsNullOrWhiteSpace(item5Name))
                        items.Add($"{item5Name} x {env.ObjVerEx.GetPropertyAsDouble(Configuration.ShiftAllocation_Qty5HBC) ?? 0}");

                    string itemsList = string.Join(Environment.NewLine, items);

                    string body = $"You have been assigned to a shift\n\n" +
                                 $"Resident: {residentName}\n" +
                                 $"Address: {residentAddress}\n" +
                                 $"\nStaff Assigned:\n{staffMembers}\n" +
                                 $"Date: {local_Start_DateTime.ToString("dddd, dd MMMM yyyy")}\n" +
                                 $"Time: {local_Start_DateTime.ToString("HH:mm")} - {local_End_DateTime.ToString("HH:mm")}\n" +
                                 $"Duration: {totalTimeInMinutes} minutes\n\n" +
                                 $"{itemsList}\n\n" +
                                 $"Please confirm your attendance.";

                    var emailFreq = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_RecurrenceFrequency).TypedValue.GetValueAsLookup();
                    var recurrenceEndDate = env.ObjVerEx.HasValue(Configuration.ShiftAllocation_RecurrenceEndDate) ? env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_RecurrenceEndDate).TypedValue.GetValueAsTimestamp().ToDateTime() : local_Start_DateTime.AddYears(1);

                    var recurrence = new RecurrencePattern() { Until = recurrenceEndDate };

                    //Can't use Configuration.EmailFrequency_Daily.Guid because not const at compile time
                    switch (emailFreq?.ItemGUID)
                    {
                        case Configuration.EmailFrequency_DailyGUID:
                            recurrence.Frequency = RecurrenceFrequency.Daily;
                            break;
                        case Configuration.EmailFrequency_WeeklyGUID:
                            if (!env.ObjVerEx.HasValue(Configuration.ShiftAllocation_DaysOfWeek))
                                throw new Exception("Days of Week must have at least 1 value specified for the selected Email Frequency.");

                            var emailRecurrenceInterval = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_RecurrenceInterval).TypedValue.GetValueAsLookup();

                            switch (emailRecurrenceInterval?.ItemGUID)
                            {
                                case Configuration.Email_RecurrenceInterval_BiWeeklyGUID:
                                    recurrence.Interval = 2;
                                    break;
                                case Configuration.Email_RecurrenceInterval_TriWeeklyGUID:
                                    recurrence.Interval = 3;
                                    break;
                                default:
                                    recurrence.Interval = 1;
                                    break;
                            }

                            recurrence.Frequency = RecurrenceFrequency.Weekly;
                            var daysOfWeek = env.ObjVerEx.GetPropertyAsValueListItems(Configuration.ShiftAllocation_DaysOfWeek);
                            foreach (var dow in daysOfWeek)
                            {
                                recurrence.DaysOfWeek.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dow.Name));
                            }

                            break;
                        case Configuration.EmailFrequency_MonthlyGUID:
                            if (!env.ObjVerEx.HasValue(Configuration.ShiftAllocation_DayOfMonth))
                                throw new Exception("Day of Month must be specified for the selected Email Frequency.");

                            recurrence.Frequency = RecurrenceFrequency.Monthly;
                            var dayOfMonth = env.ObjVerEx.GetPropertyAsValueListItem(Configuration.ShiftAllocation_DayOfMonth);
                            recurrence.DayOfMonth = int.Parse(dayOfMonth.Name);

                            break;
                        default:
                            recurrence.Frequency = RecurrenceFrequency.None;
                            break;
                    }

                    foreach (string emailAddress in staffEmailAddresses.Distinct())
                    {
                        emailService.SendEmailWithCalendarInvite(
                                emailAddress,
                                subject,
                                body,
                                location,
                                local_Start_DateTime,
                                local_End_DateTime,
                                recurrence,
                                calendarCategories.Distinct().ToList()
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<ObjVer> GetTBCSItemsByResident(ObjVerEx resident, MFIdentifier alias)
        {
            List<ObjVer> objVers = new List<ObjVer>();

            Lookups tbcScheduleItems = resident.GetLookups(alias);

            foreach (Lookup item in tbcScheduleItems)
            {
                objVers.Add(item.GetAsObjVer());
            }

            return objVers;
        }

        private MFIdentifier GetADLAliasForDayOfWeek(DateTime dateToCheck)
        {
            var dayOfWeek = dateToCheck.DayOfWeek;

            switch (dateToCheck.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return Configuration.SundayADLLookup;
                case DayOfWeek.Monday:
                    return Configuration.MondayADLLookup;
                case DayOfWeek.Tuesday:
                    return Configuration.TuesdayADLLookup;
                case DayOfWeek.Wednesday:
                    return Configuration.WednesdayADLLookup;
                case DayOfWeek.Thursday:
                    return Configuration.ThursdayADLLookup;
                case DayOfWeek.Friday:
                    return Configuration.FridayADLLookup;
                case DayOfWeek.Saturday:
                    return Configuration.SaturdayADLLookup;
                default:
                    return Configuration.SundayADLLookup;
            }
        }

        private void UpdateSiteStockForShiftAllocation(EventHandlerEnvironment env, int siteIdFromResident, SiteStockUpdateService siteStockUpdateService, MFIdentifier itemAlias, MFIdentifier quantityAlias)
        {
            int itemStockId = env.ObjVerEx.GetLookupID(itemAlias);
            if (itemStockId > -1)
            {
                string itemName = env.ObjVerEx.GetPropertyText(itemAlias);
                double itemQuantity = env.ObjVerEx.GetPropertyAsDouble(quantityAlias) ?? 0;
                siteStockUpdateService.UpdateSiteStock(siteIdFromResident, itemStockId, -itemQuantity, itemName, true);
            }
        }

    }
}
