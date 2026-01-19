using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.DailyCare")]
        public void BeforeCreateNewDailyCare(EventHandlerEnvironment env)
        {
            if (CheckAlreadyExists(env, Configuration.DailyCareClass))
            {
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to report 1.5 under the browse section for the existing records.");
            }

            SetScheduledTimeBasedCare(env);
            SetCarePlanNotes(env);

            env.ObjVerEx.SaveProperties();
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MDD")]
        public void BeforeCreateNewMedsDosageDispense(EventHandlerEnvironment env)
        {
            SetMedsDosageDispenseDefaults(env.Vault, env.ObjVerEx);
        }

        private void SetMedsDosageDispenseDefaults(Vault vault, ObjVerEx objVerEx)
        {
            Lookup medsListLookup = objVerEx.GetProperty(Configuration.MedicineList).TypedValue.GetValueAsLookup();
            ObjVerEx medsList = new ObjVerEx(vault, medsListLookup);

            string genericName = medsList.GetPropertyText(Configuration.MedsGiven_GenericName);
            string tradeName = medsList.GetPropertyText(Configuration.MedsGiven_TradeName);

            objVerEx.SetProperty(Configuration.MedsGiven_GenericName, MFDataType.MFDatatypeText, genericName);
            objVerEx.SetProperty(Configuration.MedsGiven_TradeName, MFDataType.MFDatatypeText, tradeName);
            objVerEx.SaveProperties();
        }

        private void RunExports(ObjVerEx dailyCare)
        {
            if (dailyCare.GetLookupID(Configuration.DailyCare_NoteType) == Configuration.DailyCare_AdmissionNoteType.ID)
            {
                ExportQMRAdmission(dailyCare);
            }
        }
        private void ExportQMRAdmission(ObjVerEx dailyCare)
        {

        }
        private bool CheckAlreadyExists(EventHandlerEnvironment env, MFIdentifier classToCheck)
        {
            int residentId = env.ObjVerEx.GetLookupID(Configuration.ResidentLookup);
            string shift = env.ObjVerEx.GetPropertyText(Configuration.Shift);

            DailyCareSearchService searchService = new DailyCareSearchService(env.Vault, Configuration);
            ObjVerEx dailyCare = searchService.GetDailyCareByResidentAndShift(residentId, shift, classToCheck);

            if (dailyCare != null)
            {
                return true;
            }

            return false;
        }
        private void SetResidentCareDoneForShift(EventHandlerEnvironment env)
        {
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);
            resident.CheckOut();
            resident.SaveProperty(Configuration.CareDoneForShift, MFDataType.MFDatatypeBoolean, true);
            resident.CheckIn();
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.ProgressNote")]
        public void AfterCreateNewProgressNote(EventHandlerEnvironment env)
        {
            List<int> residentUpdateTypes = new List<int>
            {
                Configuration.DailyCare_BackInResidenceNoteType.ID,
                Configuration.DailyCare_DeceasedNoteType.ID,
                Configuration.DailyCare_DischargedNoteType.ID,
                Configuration.DailyCare_HospitalNoteType.ID,
                Configuration.DailyCare_TempDischargeNoteType.ID
            };

            if (residentUpdateTypes.Contains(env.ObjVerEx.GetLookupID(Configuration.DailyCare_NoteType)))
            {
                UpdateResidentStatusFromProgressNote(env.Vault, env.ObjVerEx);
            }

            ProgressNoteSummaryUpdateService service = new ProgressNoteSummaryUpdateService(env.Vault, Configuration);
            service.LogProgressNoteCreation(env.ObjVerEx);

            ExportProgressNote(env.Vault, env.ObjVerEx);
        }

        private void UpdateResidentStatusFromProgressNote(Vault vault, ObjVerEx note)
        {
            ObjVerEx resident = new ObjVerEx(vault, note.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup());

            int typeId = note.GetLookupID(Configuration.DailyCare_NoteType);

            if (typeId == Configuration.DailyCare_BackInResidenceNoteType.ID)
            {
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.ReturnedToResidenceListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();

            }
            else if (typeId == Configuration.DailyCare_DeceasedNoteType.ID)
            {
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.DeceasedListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SetProperty(Configuration.Active, MFDataType.MFDatatypeBoolean, false);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_DischargedNoteType.ID)
            {
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.DischargedListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SetProperty(Configuration.Active, MFDataType.MFDatatypeBoolean, false);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_HospitalNoteType.ID)
            {
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.HospitalListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_TempDischargeNoteType.ID)
            {
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.TempDischargeListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();
            }
        }

        public void ExportProgressNote(Vault vault, ObjVerEx note)
        {
            IncidentExportService exportService = new IncidentExportService(vault, Configuration);

            if (note.GetLookupID(Configuration.DailyCare_NoteType) == Configuration.DailyCare_IncidentNoteType.ID)
            {
                exportService.ExportIncident(note);
            }
        }


        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.DailyCareCopy")]
        public void BeforeCreateNewDailyCareV2(EventHandlerEnvironment env)
        {
            if (CheckAlreadyExists(env, Configuration.DailyCare_CareClass))
            {
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to Daily Care not yet Complete.");
            }

            SetScheduledTimeBasedCare(env);
            env.ObjVerEx.SaveProperties();

            SetScheduledTimeSlotsUsingScheduledCareItem(env);
            env.ObjVerEx.SaveProperties();

            SetCarePlanNotes(env);
            env.ObjVerEx.SaveProperties();
        }

        private void AddSlotsToNewProperties(EventHandlerEnvironment env, params List<ObjVer>[] slots)
        {
            var properties = new[]
            {
        Configuration.TBCS_0000_0100CareItem, Configuration.TBCS_0100_0200CareItem, Configuration.TBCS_0200_0300CareItem, Configuration.TBCS_0300_0400CareItem,
        Configuration.TBCS_0400_0500CareItem, Configuration.TBCS_0500_0600CareItem, Configuration.TBCS_0600_0700CareItem, Configuration.TBCS_0700_0800CareItem,
        Configuration.TBCS_0800_0900CareItem, Configuration.TBCS_0900_1000CareItem, Configuration.TBCS_1000_1100CareItem, Configuration.TBCS_1100_1200CareItem,
        Configuration.TBCS_1200_1300CareItem, Configuration.TBCS_1300_1400CareItem, Configuration.TBCS_1400_1500CareItem, Configuration.TBCS_1500_1600CareItem,
        Configuration.TBCS_1600_1700CareItem, Configuration.TBCS_1700_1800CareItem, Configuration.TBCS_1800_1900CareItem, Configuration.TBCS_1900_2000CareItem,
        Configuration.TBCS_2000_2100CareItem, Configuration.TBCS_2100_2200CareItem, Configuration.TBCS_2200_2300CareItem, Configuration.TBCS_2300_0000CareItem
    };

            //Seems like logic might not be right, because of using length instead of fixed 24 properties, especialy slots.length
            for (int i = 0; i < slots.Length && i < properties.Length; i++)
            {
                slots[i].ForEach(x => env.ObjVerEx.AddLookup(properties[i], x));
            }
        }

        private void AddTolots(ObjVer item, params List<ObjVer>[] slots)
        {
            foreach (var slot in slots)
            {
                slot.Add(item);
            }
        }

        private void SetScheduledTimeSlotsUsingScheduledCareItem(EventHandlerEnvironment env)
        {
            // Initialize time slot lists
            List<ObjVer> slot_01 = new List<ObjVer>();
            List<ObjVer> slot_12 = new List<ObjVer>();
            List<ObjVer> slot_23 = new List<ObjVer>();
            List<ObjVer> slot_34 = new List<ObjVer>();
            List<ObjVer> slot_45 = new List<ObjVer>();
            List<ObjVer> slot_56 = new List<ObjVer>();
            List<ObjVer> slot_67 = new List<ObjVer>();
            List<ObjVer> slot_78 = new List<ObjVer>();
            List<ObjVer> slot_89 = new List<ObjVer>();
            List<ObjVer> slot_910 = new List<ObjVer>();
            List<ObjVer> slot_1011 = new List<ObjVer>();
            List<ObjVer> slot_1112 = new List<ObjVer>();
            List<ObjVer> slot_1213 = new List<ObjVer>();
            List<ObjVer> slot_1314 = new List<ObjVer>();
            List<ObjVer> slot_1415 = new List<ObjVer>();
            List<ObjVer> slot_1516 = new List<ObjVer>();
            List<ObjVer> slot_1617 = new List<ObjVer>();
            List<ObjVer> slot_1718 = new List<ObjVer>();
            List<ObjVer> slot_1819 = new List<ObjVer>();
            List<ObjVer> slot_1920 = new List<ObjVer>();
            List<ObjVer> slot_2021 = new List<ObjVer>();
            List<ObjVer> slot_2122 = new List<ObjVer>();
            List<ObjVer> slot_2223 = new List<ObjVer>();
            List<ObjVer> slot_2300 = new List<ObjVer>();

            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            SiteSearchService siteSearchService = new SiteSearchService(env.Vault, Configuration);
            ObjVerEx siteConfig = siteSearchService.GetSiteConfig(resident.GetLookupID(Configuration.Resident_Site));

            Lookups items = env.ObjVerEx.GetProperty(Configuration.TBCS_TimeBasedCareScheduleDropdown).TypedValue.GetValueAsLookups();

            foreach (Lookup item in items)
            {
                ObjVerEx careItem = new ObjVerEx(env.Vault, item);
                var timeBasedCareItemLookup = careItem.GetProperty(Configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
                ObjVer timeBasedCareItem = timeBasedCareItemLookup.GetAsObjVer();

                var hasFrequency = careItem.HasProperty(Configuration.TBCS_Frequency) && careItem.HasValue(Configuration.TBCS_Frequency);

                if (hasFrequency && careItem.GetLookupID(Configuration.TBCS_Frequency) == Configuration.Frequency_OnceOff.ID)
                {
                    var onceOffDate = careItem.GetPropertyAsDateTime(Configuration.TBCS_OnceOffDate);

                    if (onceOffDate == null)
                        continue;

                    if (onceOffDate.Value.Date == DateTime.Now.Date)
                    {
                        slot_89.Add(timeBasedCareItem);
                    }
                }
                else if (hasFrequency && !(careItem.GetLookupID(Configuration.TBCS_Frequency) == Configuration.Frequency_SpecificTimes.ID))
                {
                    int frequencyId = careItem.GetLookupID(Configuration.TBCS_Frequency);

                    if (frequencyId == Configuration.Frequency_Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_12, slot_23, slot_34, slot_45, slot_56, slot_67, slot_78, slot_89, slot_910, slot_1011, slot_1112, slot_1213, slot_1314, slot_1415, slot_1516, slot_1617, slot_1718, slot_1819, slot_1920, slot_2021, slot_2122, slot_2223, slot_2300);
                    }
                    else if (frequencyId == Configuration.Frequency_2Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_23, slot_45, slot_67, slot_89, slot_1011, slot_1213, slot_1415, slot_1617, slot_1819, slot_2021, slot_2223);
                    }
                    else if (frequencyId == Configuration.Frequency_3Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_34, slot_67, slot_910, slot_1213, slot_1516, slot_1819, slot_2122);
                    }
                    else if (frequencyId == Configuration.Frequency_4Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_45, slot_89, slot_1213, slot_1617, slot_2021);
                    }
                    else if (frequencyId == Configuration.Frequency_6Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_67, slot_1213, slot_1819);
                    }
                    else if (frequencyId == Configuration.Frequency_8Hourly.ID)
                    {
                        AddTolots(timeBasedCareItem, slot_01, slot_89, slot_1617);
                    }
                    else if (frequencyId == Configuration.Frequency_Daily.ID)
                    {
                        slot_89.Add(timeBasedCareItem);
                    }
                    else if (frequencyId == Configuration.Frequency_Weekly.ID)
                    {
                        int SCDayOfWeek = siteConfig != null && siteConfig.HasValue(Configuration.SiteConfig_SCDayOfWeek) ? siteConfig.GetProperty(Configuration.SiteConfig_SCDayOfWeek).GetValue<int>() : 1;

                        if ((int)DateTime.Now.DayOfWeek == SCDayOfWeek)
                            slot_89.Add(timeBasedCareItem);
                    }
                    else if (frequencyId == Configuration.Frequency_Monthly.ID)
                    {
                        int SCDayOfMonth = siteConfig != null && siteConfig.HasValue(Configuration.SiteConfig_SCDayOfMonth) ? siteConfig.GetProperty(Configuration.SiteConfig_SCDayOfMonth).GetValue<int>() : 1;
                        if (DateTime.Now.Day == SCDayOfMonth)
                            slot_89.Add(timeBasedCareItem);
                    }
                }
                else
                {
                    AddSpecificTimeSlots(careItem, timeBasedCareItem, slot_01, slot_12, slot_23, slot_34, slot_45, slot_56, slot_67, slot_78, slot_89, slot_910, slot_1011, slot_1112, slot_1213, slot_1314, slot_1415, slot_1516, slot_1617, slot_1718, slot_1819, slot_1920, slot_2021, slot_2122, slot_2223, slot_2300);
                }
            }

            // Add all slots to new properties
            AddSlotsToNewProperties(env, slot_01, slot_12, slot_23, slot_34, slot_45, slot_56, slot_67, slot_78, slot_89, slot_910, slot_1011, slot_1112, slot_1213, slot_1314, slot_1415, slot_1516, slot_1617, slot_1718, slot_1819, slot_1920, slot_2021, slot_2122, slot_2223, slot_2300);
        }

        private List<ObjVer> SetScheduledTimeBasedCare(EventHandlerEnvironment env)
        {
            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            SiteSearchService siteSearchService = new SiteSearchService(env.Vault, Configuration);
            ObjVerEx siteConfig = siteSearchService.GetSiteConfig(resident.GetLookupID(Configuration.Resident_Site));
            bool useCarePlan = siteConfig != null && siteConfig.HasValue(Configuration.SiteConfig_TbcFromCarePlan)
                && siteConfig.GetProperty(Configuration.SiteConfig_TbcFromCarePlan).GetValue<bool>();

            List<ObjVer> TBCADL = residentPropertyService.GetResidentTBCSForDay(residentLookup, useCarePlan);

            TBCADL.ForEach(x =>
            {
                env.ObjVerEx.AddLookup(Configuration.TBCS_TimeBasedCareScheduleDropdown, x);
            });

            return TBCADL;
        }

        private void SetCarePlanNotes(EventHandlerEnvironment env)
        {
            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
            int lookupId = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID();

            ObjVerEx careplan = searchService.GetResidentCarePlanExisting(lookupId);

            string output = careplan == null ? "" : $"{careplan.GetPropertyText(Configuration.Careplan_CpDietAndFeeding)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpToilet)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpPsychosocialSummary)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpWalkingAids)}";

            env.ObjVerEx.SaveProperty(Configuration.DailyCare_CarePlanNotes, MFDataType.MFDatatypeMultiLineText, output);
        }

        private void AddSpecificTimeSlots(ObjVerEx scheduleItem, ObjVer timeBasedCareItem, params List<ObjVer>[] allSlots)
        {
            var timeSlotMap = new Dictionary<int, int>
    {
        { Configuration.ScheduledCareTime_0000.ID, 0 },
        { Configuration.ScheduledCareTime_0100.ID, 1 },
        { Configuration.ScheduledCareTime_0200.ID, 2 },
        { Configuration.ScheduledCareTime_0300.ID, 3 },
        { Configuration.ScheduledCareTime_0400.ID, 4 },
        { Configuration.ScheduledCareTime_0500.ID, 5 },
        { Configuration.ScheduledCareTime_0600.ID, 6 },
        { Configuration.ScheduledCareTime_0700.ID, 7 },
        { Configuration.ScheduledCareTime_0800.ID, 8 },
        { Configuration.ScheduledCareTime_0900.ID, 9 },
        { Configuration.ScheduledCareTime_1000.ID, 10 },
        { Configuration.ScheduledCareTime_1100.ID, 11 },
        { Configuration.ScheduledCareTime_1200.ID, 12 },
        { Configuration.ScheduledCareTime_1300.ID, 13 },
        { Configuration.ScheduledCareTime_1400.ID, 14 },
        { Configuration.ScheduledCareTime_1500.ID, 15 },
        { Configuration.ScheduledCareTime_1600.ID, 16 },
        { Configuration.ScheduledCareTime_1700.ID, 17 },
        { Configuration.ScheduledCareTime_1800.ID, 18 },
        { Configuration.ScheduledCareTime_1900.ID, 19 },
        { Configuration.ScheduledCareTime_2000.ID, 20 },
        { Configuration.ScheduledCareTime_2100.ID, 21 },
        { Configuration.ScheduledCareTime_2200.ID, 22 },
        { Configuration.ScheduledCareTime_2300.ID, 23 }
    };

            if (!scheduleItem.HasValue(Configuration.TBCS_TbcScheduledTimes))
            {
                return;
            }

            foreach (Lookup time in scheduleItem.GetLookups(Configuration.TBCS_TbcScheduledTimes))
            {
                if (timeSlotMap.ContainsKey(time.Item))
                {
                    allSlots[timeSlotMap[time.Item]].Add(timeBasedCareItem);
                }
            }
        }


    }
}
