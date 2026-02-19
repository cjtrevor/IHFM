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
            var server_Start_Timestamp = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_StartDateTime).TypedValue.GetValueAsTimestamp();
            var local_Start_DateTime = server_Start_Timestamp.ToLocalDateTime();

            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ShiftAllocation_Resident).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
            ObjVerEx careplan = searchService.GetResidentCarePlanExisting(residentLookup.Item);

            List<ObjVer> objVers = new List<ObjVer>();

            if (careplan != null)
            {
                objVers.AddRange(GetTBCSItemsByResident(resident, Configuration.DailyADLLookup));
                objVers.AddRange(GetTBCSItemsByResident(resident, GetADLAliasForDayOfWeek(local_Start_DateTime)));
            }

            int totalTimeInMinutes = 0;

            foreach (ObjVer item in objVers)
            {
                var objVerEx = new ObjVerEx(env.Vault, item);

                var timeBasedCareItemLookup = objVerEx.GetProperty(Configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
                ObjVer timeBasedCareItem = timeBasedCareItemLookup.GetAsObjVer();
                var timeBasedCareItemVerEx = new ObjVerEx(env.Vault, timeBasedCareItem);

                Int32.TryParse(timeBasedCareItemVerEx.GetPropertyText(Configuration.AverageTime), out int time);
                totalTimeInMinutes += time;
            }

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

                if (totalTimeInMinutes < 20)
                    return;

                string staffMembers = "";
                var staffEmailAddresses = new List<string>();
                staffEmailAddresses.AddRange(Configuration.ShiftAllocation_MailLising.Split(';'));

                foreach (Lookup staffLookup in staffAttendingLookups)
                {
                    var staffObjVer = new ObjVerEx(env.Vault, staffLookup);
                    var emailAddress = staffObjVer.GetPropertyText(Configuration.Staff_EmailAddress);
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

                    string body = $"You have been assigned to a shift\n\n" +
                                 $"Resident: {residentName}\n" +
                                 $"Address: {residentAddress}\n" +
                                 $"\nStaff Assigned:\n{staffMembers}\n" +
                                 $"Date: {local_Start_DateTime.ToString("dddd, dd MMMM yyyy")}\n" +
                                 $"Time: {local_Start_DateTime.ToString("HH:mm")} - {local_End_DateTime.ToString("HH:mm")}\n" +
                                 $"Duration: {totalTimeInMinutes} minutes\n\n" +
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

                    foreach (string emailAddress in staffEmailAddresses)
                    {
                        emailService.SendEmailWithCalendarInvite(
                                emailAddress,
                                subject,
                                body,
                                location,
                                local_Start_DateTime,
                                local_End_DateTime,
                                recurrence
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
    }
}
