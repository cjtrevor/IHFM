using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
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
            DailyCareLogger.Log($"BeforeCreateNewDailyCare START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("BeforeCreateNewDailyCare — calling CheckAlreadyExists");
            if (CheckAlreadyExists(env, Configuration.DailyCare_DailyCareClass))
            {
                DailyCareLogger.Log("BeforeCreateNewDailyCare — duplicate found, throwing");
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to Daily Care not yet Complete.");
            }

            //SetScheduledTimeBasedCare(env);

            //SetCarePlanNotes(env);

            //env.ObjVerEx.SaveProperties();
            sw.Stop();
            DailyCareLogger.Log($"BeforeCreateNewDailyCare END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.DailyCareCopy")]
        public void BeforeCreateNewDailyCareV2(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log($"BeforeCreateNewDailyCareV2 START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — calling CheckAlreadyExists");
            if (CheckAlreadyExists(env, Configuration.DailyCare_CareClass))
            {
                DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — duplicate found, throwing");
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to Daily Care not yet Complete.");
            }

            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — calling SetScheduledTimeBasedCare");
            SetScheduledTimeBasedCare(env);
            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — SaveProperties #1");
            env.ObjVerEx.SaveProperties();

            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — calling SetScheduledTimeSlots");
            SetScheduledTimeSlots(env);
            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — SaveProperties #2");
            env.ObjVerEx.SaveProperties();

            DailyCareLogger.Log("BeforeCreateNewDailyCareV2 — calling SetCarePlanNotes");
            SetCarePlanNotes(env);

            sw.Stop();
            DailyCareLogger.Log($"BeforeCreateNewDailyCareV2 END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void SetScheduledTimeSlots(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log("SetScheduledTimeSlots START");
            var sw = System.Diagnostics.Stopwatch.StartNew();
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

                if (careItem.HasProperty(Configuration.TBCS_Frequency) && careItem.HasValue(Configuration.TBCS_Frequency)
                    && !(careItem.GetLookupID(Configuration.TBCS_Frequency) == Configuration.Frequency_SpecificTimes.ID))
                {
                    int frequencyId = careItem.GetLookupID(Configuration.TBCS_Frequency);

                    if (frequencyId == Configuration.Frequency_Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_12.Add(item.GetAsObjVer());
                        slot_23.Add(item.GetAsObjVer());
                        slot_34.Add(item.GetAsObjVer());
                        slot_45.Add(item.GetAsObjVer());
                        slot_56.Add(item.GetAsObjVer());
                        slot_67.Add(item.GetAsObjVer());
                        slot_78.Add(item.GetAsObjVer());
                        slot_89.Add(item.GetAsObjVer());
                        slot_910.Add(item.GetAsObjVer());
                        slot_1011.Add(item.GetAsObjVer());
                        slot_1112.Add(item.GetAsObjVer());
                        slot_1213.Add(item.GetAsObjVer());
                        slot_1314.Add(item.GetAsObjVer());
                        slot_1415.Add(item.GetAsObjVer());
                        slot_1516.Add(item.GetAsObjVer());
                        slot_1617.Add(item.GetAsObjVer());
                        slot_1718.Add(item.GetAsObjVer());
                        slot_1819.Add(item.GetAsObjVer());
                        slot_1920.Add(item.GetAsObjVer());
                        slot_2021.Add(item.GetAsObjVer());
                        slot_2122.Add(item.GetAsObjVer());
                        slot_2223.Add(item.GetAsObjVer());
                        slot_2300.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_2Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_23.Add(item.GetAsObjVer());
                        slot_45.Add(item.GetAsObjVer());
                        slot_67.Add(item.GetAsObjVer());
                        slot_89.Add(item.GetAsObjVer());
                        slot_1011.Add(item.GetAsObjVer());
                        slot_1213.Add(item.GetAsObjVer());
                        slot_1415.Add(item.GetAsObjVer());
                        slot_1617.Add(item.GetAsObjVer());
                        slot_1819.Add(item.GetAsObjVer());
                        slot_2021.Add(item.GetAsObjVer());
                        slot_2223.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_3Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_34.Add(item.GetAsObjVer());
                        slot_67.Add(item.GetAsObjVer());
                        slot_910.Add(item.GetAsObjVer());
                        slot_1213.Add(item.GetAsObjVer());
                        slot_1516.Add(item.GetAsObjVer());
                        slot_1819.Add(item.GetAsObjVer());
                        slot_2122.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_4Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_45.Add(item.GetAsObjVer());
                        slot_89.Add(item.GetAsObjVer());
                        slot_1213.Add(item.GetAsObjVer());
                        slot_1617.Add(item.GetAsObjVer());
                        slot_2021.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_6Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_67.Add(item.GetAsObjVer());
                        slot_1213.Add(item.GetAsObjVer());
                        slot_1819.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_8Hourly.ID)
                    {
                        slot_01.Add(item.GetAsObjVer());
                        slot_89.Add(item.GetAsObjVer());
                        slot_1617.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_Daily.ID)
                    {
                        slot_89.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_Weekly.ID)
                    {
                        int SCDayOfWeek = siteConfig.HasValue(Configuration.SiteConfig_SCDayOfWeek) ? siteConfig.GetProperty(Configuration.SiteConfig_SCDayOfWeek).GetValue<int>() : 1;

                        if ((int)DateTime.Now.DayOfWeek == SCDayOfWeek)
                            slot_89.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_Monthly.ID)
                    {
                        int SCDayOfMonth = siteConfig.HasValue(Configuration.SiteConfig_SCDayOfMonth) ? siteConfig.GetProperty(Configuration.SiteConfig_SCDayOfMonth).GetValue<int>() : 1;
                        if (DateTime.Now.Day == SCDayOfMonth)
                            slot_89.Add(item.GetAsObjVer());
                    }
                }
                else
                {
                    //Specific times logic to be used
                    foreach (Lookup time in careItem.GetLookups(Configuration.TBCS_TbcScheduledTimes))
                    {
                        if (time.Item == Configuration.ScheduledCareTime_0000.ID)
                        {
                            slot_01.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0100.ID)
                        {
                            slot_12.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0200.ID)
                        {
                            slot_23.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0300.ID)
                        {
                            slot_34.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0400.ID)
                        {
                            slot_45.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0500.ID)
                        {
                            slot_56.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0600.ID)
                        {
                            slot_67.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0700.ID)
                        {
                            slot_78.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0800.ID)
                        {
                            slot_89.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_0900.ID)
                        {
                            slot_910.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1000.ID)
                        {
                            slot_1011.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1100.ID)
                        {
                            slot_1112.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1200.ID)
                        {
                            slot_1213.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1300.ID)
                        {
                            slot_1314.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1400.ID)
                        {
                            slot_1415.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1500.ID)
                        {
                            slot_1516.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1600.ID)
                        {
                            slot_1617.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1700.ID)
                        {
                            slot_1718.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1800.ID)
                        {
                            slot_1819.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1900.ID)
                        {
                            slot_1920.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_2000.ID)
                        {
                            slot_2021.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_2100.ID)
                        {
                            slot_2122.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_2200.ID)
                        {
                            slot_2223.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_2300.ID)
                        {
                            slot_2300.Add(item.GetAsObjVer());
                        }
                    }
                }
            }
            slot_01.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0000_0100Care, x);
            });
            slot_12.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0100_0200Care, x);
            });
            slot_23.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0200_0300Care, x);
            });
            slot_34.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0300_0400Care, x);
            });
            slot_45.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0400_0500Care, x);
            });
            slot_56.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0500_0600Care, x);
            });
            slot_67.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0600_0700Care, x);
            });
            slot_78.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0700_0800Care, x);
            });
            slot_89.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0800_0900Care, x);
            });
            slot_910.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0900_1000Care, x);
            });
            slot_1011.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1000_1100Care, x);
            });
            slot_1112.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1100_1200Care, x);
            });
            slot_1213.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1200_1300Care, x);
            });
            slot_1314.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1300_1400Care, x);
            });
            slot_1415.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1400_1500Care, x);
            });
            slot_1516.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1500_1600Care, x);
            });
            slot_1617.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1600_1700Care, x);
            });
            slot_1718.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1700_1800Care, x);
            });
            slot_1819.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1800_1900Care, x);
            });
            slot_1920.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1900_2000Care, x);
            });
            slot_2021.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_2000_2100Care, x);
            });
            slot_2122.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_2100_2200Care, x);
            });
            slot_2223.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_2200_2300Care, x);
            });
            slot_2300.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_2300_0000Care, x);
            });

            sw.Stop();
            DailyCareLogger.Log($"SetScheduledTimeSlots END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void SetCarePlanNotes(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log("SetCarePlanNotes START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
            int lookupId = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID();

            DailyCareLogger.Log($"SetCarePlanNotes — searching care plan for resident={lookupId}");
            ObjVerEx careplan = searchService.GetResidentCarePlanExisting(lookupId);
            DailyCareLogger.Log($"SetCarePlanNotes — care plan found={careplan != null}");

            string output = careplan == null ? "" : $"{careplan.GetPropertyText(Configuration.Careplan_CpDietAndFeeding)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpToilet)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpPsychosocialSummary)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpWalkingAids)}";

            env.ObjVerEx.SaveProperty(Configuration.DailyCare_CarePlanNotes, MFDataType.MFDatatypeMultiLineText, output);

            sw.Stop();
            DailyCareLogger.Log($"SetCarePlanNotes END — elapsed={sw.ElapsedMilliseconds}ms");
        }
        private void SetScheduledTimeBasedCare(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log("SetScheduledTimeBasedCare START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            DailyCareLogger.Log("SetScheduledTimeBasedCare — fetching site config");
            SiteSearchService siteSearchService = new SiteSearchService(env.Vault, Configuration);
            ObjVerEx siteConfig = siteSearchService.GetSiteConfig(resident.GetLookupID(Configuration.Resident_Site));
            bool useCarePlan = siteConfig.HasValue(Configuration.SiteConfig_TbcFromCarePlan)
                && siteConfig.GetProperty(Configuration.SiteConfig_TbcFromCarePlan).GetValue<bool>();

            DailyCareLogger.Log($"SetScheduledTimeBasedCare — useCarePlan={useCarePlan}, calling GetResidentTBCSForDay");
            List<ObjVer> TBCADL = residentPropertyService.GetResidentTBCSForDay(residentLookup, useCarePlan);
            DailyCareLogger.Log($"SetScheduledTimeBasedCare — TBC items retrieved={TBCADL.Count}");

            TBCADL.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_TimeBasedCareScheduleDropdown, x);
            });

            sw.Stop();
            DailyCareLogger.Log($"SetScheduledTimeBasedCare END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void RunExports(ObjVerEx dailyCare)
        {
            if(dailyCare.GetLookupID(Configuration.DailyCare_NoteType) == Configuration.DailyCare_AdmissionNoteType.ID)
            {
                ExportQMRAdmission(dailyCare);
            }
        }
        private void ExportQMRAdmission(ObjVerEx dailyCare)
        {

        }
        private bool CheckAlreadyExists(EventHandlerEnvironment env, MFIdentifier classToCheck)
        {
            DailyCareLogger.Log("CheckAlreadyExists START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            int residentId = env.ObjVerEx.GetLookupID(Configuration.ResidentLookup);
            string shift = env.ObjVerEx.GetPropertyText(Configuration.Shift);

            DailyCareLogger.Log($"CheckAlreadyExists — resident={residentId}, shift={shift}");
            DailyCareSearchService searchService = new DailyCareSearchService(env.Vault, Configuration);
            ObjVerEx dailyCare = searchService.GetDailyCareByResidentAndShift(residentId, shift, classToCheck);

            sw.Stop();
            DailyCareLogger.Log($"CheckAlreadyExists END — found={dailyCare != null}, elapsed={sw.ElapsedMilliseconds}ms");

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
            DailyCareLogger.Log($"AfterCreateNewProgressNote START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            List<int> residentUpdateTypes = new List<int>
            {
                Configuration.DailyCare_BackInResidenceNoteType.ID,
                Configuration.DailyCare_DeceasedNoteType.ID,
                Configuration.DailyCare_DischargedNoteType.ID,
                Configuration.DailyCare_HospitalNoteType.ID,
                Configuration.DailyCare_TempDischargeNoteType.ID
            };

            DailyCareLogger.Log("AfterCreateNewProgressNote — checking note type for resident status update");
            if(residentUpdateTypes.Contains(env.ObjVerEx.GetLookupID(Configuration.DailyCare_NoteType)))
            {
                DailyCareLogger.Log("AfterCreateNewProgressNote — calling UpdateResidentStatusFromProgressNote");
                UpdateResidentStatusFromProgressNote(env.Vault, env.ObjVerEx);
                DailyCareLogger.Log("AfterCreateNewProgressNote — UpdateResidentStatusFromProgressNote done");
            }

            DailyCareLogger.Log("AfterCreateNewProgressNote — calling LogProgressNoteCreation");
            ProgressNoteSummaryUpdateService service = new ProgressNoteSummaryUpdateService(env.Vault, Configuration);
            service.LogProgressNoteCreation(env.ObjVerEx);
            DailyCareLogger.Log("AfterCreateNewProgressNote — LogProgressNoteCreation done");

            DailyCareLogger.Log("AfterCreateNewProgressNote — calling ExportProgressNote");
            ExportProgressNote(env.Vault, env.ObjVerEx);

            sw.Stop();
            DailyCareLogger.Log($"AfterCreateNewProgressNote END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateResidentStatusFromProgressNote(Vault vault, ObjVerEx note)
        {
            DailyCareLogger.Log("UpdateResidentStatusFromProgressNote START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            ObjVerEx resident = new ObjVerEx(vault, note.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup());

            int typeId = note.GetLookupID(Configuration.DailyCare_NoteType);
            DailyCareLogger.Log($"UpdateResidentStatusFromProgressNote — typeId={typeId}");

            if (typeId == Configuration.DailyCare_BackInResidenceNoteType.ID)
            {
                DailyCareLogger.Log("UpdateResidentStatusFromProgressNote — setting BackInResidence");
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.ReturnedToResidenceListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();

            }
            else if (typeId == Configuration.DailyCare_DeceasedNoteType.ID)
            {
                DailyCareLogger.Log("UpdateResidentStatusFromProgressNote — setting Deceased");
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.DeceasedListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SetProperty(Configuration.Active, MFDataType.MFDatatypeBoolean, false);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_DischargedNoteType.ID)
            {
                DailyCareLogger.Log("UpdateResidentStatusFromProgressNote — setting Discharged");
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.DischargedListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SetProperty(Configuration.Active, MFDataType.MFDatatypeBoolean, false);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_HospitalNoteType.ID)
            {
                DailyCareLogger.Log("UpdateResidentStatusFromProgressNote — setting Hospital");
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.HospitalListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();
            }
            else if (typeId == Configuration.DailyCare_TempDischargeNoteType.ID)
            {
                DailyCareLogger.Log("UpdateResidentStatusFromProgressNote — setting TempDischarge");
                resident.SetProperty(Configuration.Resident_DeceasedDeparted, MFDataType.MFDatatypeLookup, Configuration.TempDischargeListItem.ID);
                resident.SetProperty(Configuration.Resident_DateDeceased, MFDataType.MFDatatypeDate, DateTime.Now);
                resident.SaveProperties();
            }

            sw.Stop();
            DailyCareLogger.Log($"UpdateResidentStatusFromProgressNote END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        public void ExportProgressNote(Vault vault, ObjVerEx note)
        {
            return;
            IncidentExportService exportService = new IncidentExportService(vault, Configuration);

            if (note.GetLookupID(Configuration.DailyCare_NoteType) == Configuration.DailyCare_IncidentNoteType.ID)
            {
                exportService.ExportIncident(note);
            }
        }
    }
}
