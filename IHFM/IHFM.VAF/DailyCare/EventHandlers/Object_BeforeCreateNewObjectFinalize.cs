using MFiles.VAF.Common;
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
            if (CheckAlreadyExists(env))
            {
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to Daily Care not yet Complete.");
            }

            SetScheduledTimeBasedCare(env);

            SetCarePlanNotes(env);

            env.ObjVerEx.SaveProperties();
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.DailyCareCopy")]
        public void BeforeCreateNewDailyCareV2(EventHandlerEnvironment env)
        {
            if (CheckAlreadyExists(env))
            {
                throw new Exception("A daily care for this resident record for this shift already exists. Please refer to Daily Care not yet Complete.");
            }

            SetScheduledTimeBasedCare(env);
            env.ObjVerEx.SaveProperties();

            SetScheduledTimeSlots(env);
            
            env.ObjVerEx.SaveProperties();
        }

        private void SetScheduledTimeSlots(EventHandlerEnvironment env)
        {
            List<ObjVer> slot_68 = new List<ObjVer>();
            List<ObjVer> slot_810 = new List<ObjVer>();
            List<ObjVer> slot_1012 = new List<ObjVer>();
            List<ObjVer> slot_1214 = new List<ObjVer>();
            List<ObjVer> slot_1416 = new List<ObjVer>();
            List<ObjVer> slot_1618 = new List<ObjVer>();


            Lookups items = env.ObjVerEx.GetProperty(Configuration.TBCS_TimeBasedCareScheduleDropdown).TypedValue.GetValueAsLookups();
            foreach(Lookup item in items)
            { 
                ObjVerEx careItem = new ObjVerEx(env.Vault, item);

                if(careItem.HasProperty(Configuration.TBCS_Frequency) && careItem.HasValue(Configuration.TBCS_Frequency) 
                    && !(careItem.GetLookupID(Configuration.TBCS_Frequency) == Configuration.Frequency_SpecificTimes.ID))
                {
                    int frequencyId = careItem.GetLookupID(Configuration.TBCS_Frequency);

                    if(frequencyId == Configuration.Frequency_Hourly.ID)
                    {
                        slot_68.Add(item.GetAsObjVer());
                        slot_68.Add(item.GetAsObjVer());

                        slot_810.Add(item.GetAsObjVer());
                        slot_810.Add(item.GetAsObjVer());

                        slot_1012.Add(item.GetAsObjVer());
                        slot_1012.Add(item.GetAsObjVer());

                        slot_1214.Add(item.GetAsObjVer());
                        slot_1214.Add(item.GetAsObjVer());

                        slot_1416.Add(item.GetAsObjVer());
                        slot_1416.Add(item.GetAsObjVer());

                        slot_1618.Add(item.GetAsObjVer());
                        slot_1618.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_2Hourly.ID)
                    {
                        slot_68.Add(item.GetAsObjVer());                       
                        slot_810.Add(item.GetAsObjVer());                        
                        slot_1012.Add(item.GetAsObjVer());                      
                        slot_1214.Add(item.GetAsObjVer());                       
                        slot_1416.Add(item.GetAsObjVer());                     
                        slot_1618.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_3Hourly.ID)
                    {
                        slot_68.Add(item.GetAsObjVer());
                        slot_810.Add(item.GetAsObjVer());
                        slot_1012.Add(item.GetAsObjVer());
                        slot_1214.Add(item.GetAsObjVer());
                        slot_1416.Add(item.GetAsObjVer());
                        slot_1618.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_4Hourly.ID)
                    {
                        slot_68.Add(item.GetAsObjVer());
                        slot_1012.Add(item.GetAsObjVer());
                        slot_1416.Add(item.GetAsObjVer());
                    }
                    else if (frequencyId == Configuration.Frequency_8Hourly.ID)
                    {
                        slot_68.Add(item.GetAsObjVer());
                        slot_1618.Add(item.GetAsObjVer());
                    }
                }
                else
                {
                    //Specific times logic to be used
                    foreach(Lookup time in careItem.GetLookups(Configuration.TBCS_TbcScheduledTimes))
                    {
                        if(time.Item == Configuration.ScheduledCareTime_0600.ID 
                            || time.Item == Configuration.ScheduledCareTime_0800.ID
                            || time.Item == Configuration.ScheduledCareTime_1000.ID)
                        {
                            slot_68.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1200.ID
                            || time.Item == Configuration.ScheduledCareTime_1400.ID)
                        {
                            slot_810.Add(item.GetAsObjVer());
                        }
                        else if (time.Item == Configuration.ScheduledCareTime_1600.ID
                            || time.Item == Configuration.ScheduledCareTime_1800.ID)
                        {
                            slot_1012.Add(item.GetAsObjVer());
                        }
                    }
                }
            }

            slot_68.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0600_0800Care, x);
            });

            slot_810.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_0800_1000Care, x);
            });

            slot_1012.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1000_1200Care, x);
            });

            slot_1214.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1200_1400Care, x);
            });

            slot_1416.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1400_1600Care, x);
            });

            slot_1618.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_1600_1800Care, x);
            });
        }

        private void SetCarePlanNotes(EventHandlerEnvironment env)
        {
            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
            int lookupId = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetLookupID();

            ObjVerEx careplan = searchService.GetResidentCarePlanExisting(lookupId);

            string output = careplan == null ? "" : $"{careplan.GetPropertyText(Configuration.Careplan_CpDietAndFeeding)}" +
                $"{Environment.NewLine}{careplan.GetPropertyText(Configuration.Careplan_CpToilet)}";

            env.ObjVerEx.SaveProperty(Configuration.DailyCare_CarePlanNotes, MFDataType.MFDatatypeMultiLineText, output);
        }
        private void SetScheduledTimeBasedCare(EventHandlerEnvironment env)
        {
            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            SiteSearchService siteSearchService = new SiteSearchService(env.Vault, Configuration);
            ObjVerEx siteConfig = siteSearchService.GetSiteConfig(resident.GetLookupID(Configuration.Resident_Site));
            bool useCarePlan = siteConfig.HasValue(Configuration.SiteConfig_TbcFromCarePlan)
                && siteConfig.GetProperty(Configuration.SiteConfig_TbcFromCarePlan).GetValue<bool>();

            List<ObjVer> TBCADL = residentPropertyService.GetResidentTBCSForDay(residentLookup, useCarePlan);

            TBCADL.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCS_TimeBasedCareScheduleDropdown, x);
            });

            
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
        private bool CheckAlreadyExists(EventHandlerEnvironment env)
        {
            int residentId = env.ObjVerEx.GetLookupID(Configuration.ResidentLookup);
            string shift = env.ObjVerEx.GetPropertyText(Configuration.Shift);

            DailyCareSearchService searchService = new DailyCareSearchService(env.Vault, Configuration);
            ObjVerEx dailyCare = searchService.GetDailyCareByResidentAndShift(residentId, shift);

            if(dailyCare != null)
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

            if(residentUpdateTypes.Contains(env.ObjVerEx.GetLookupID(Configuration.DailyCare_NoteType)))
            {
                UpdateResidentStatusFromProgressNote(env.Vault, env.ObjVerEx);
            }

            //ProgressNoteSummaryUpdateService service = new ProgressNoteSummaryUpdateService(env.Vault, Configuration);
            //service.LogProgressNoteCreation(env.ObjVerEx);

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
    }
}
