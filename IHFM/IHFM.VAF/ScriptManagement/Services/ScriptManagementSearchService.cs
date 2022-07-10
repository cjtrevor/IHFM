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
    public class ScriptManagementSearchService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public ScriptManagementSearchService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public ObjVerEx GetScriptManagementObject(int residentId)
        {
            MFSearchBuilder scriptSearch = new MFSearchBuilder(vault);
            scriptSearch.Class(configuration.ScriptManagementClass);
            scriptSearch.Property(configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            List<ObjVerEx> scripts = scriptSearch.FindEx();

            foreach (ObjVerEx item in scripts)
            {
                DateTime EndDate = DateTime.Parse(item.GetProperty(configuration.ScriptManagementEndDate).GetValueAsLocalizedText());

                if (EndDate > DateTime.Now)
                    return item;
            }

            return null;
        }

        public List<ObjVerEx> GetAllScriptManagementObject(int residentId)
        {
            List<ObjVerEx> activeScripts = new List<ObjVerEx>();

            MFSearchBuilder scriptSearch = new MFSearchBuilder(vault);
            scriptSearch.Class(configuration.ScriptManagementClass);
            scriptSearch.Property(configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            List<ObjVerEx> scripts = scriptSearch.FindEx();

            foreach (ObjVerEx item in scripts)
            {
                DateTime EndDate = DateTime.Parse(item.GetProperty(configuration.ScriptManagementEndDate).GetValueAsLocalizedText());

                if (EndDate > DateTime.Now)
                    activeScripts.Add(item);
            }

            return activeScripts;
        }

        public List<Lookup> GetMedsScheduledForTimeSlot(MFIdentifier slotConfig, ObjVerEx script, bool isPRN)
        {
            List<Lookup> medsToGive = new List<Lookup>();
            Lookups meds = script.GetLookups(configuration.MedsOnScript);

            foreach  (Lookup lookup in meds)
            {
                ObjVerEx med = new ObjVerEx(vault, lookup);

                //Only fail on no slotConfig if its not a 4 hour cycle record
                if (slotConfig == null && !(med.HasValue(configuration.MedsDosage_4Hourly) && med.GetProperty(configuration.MedsDosage_4Hourly).GetValue<bool>()))
                {
                    throw new Exception("The current time has no valid slot.");
                }

                //Specific Days
                if (med.HasValue(configuration.SpecificDays) && med.GetProperty(configuration.SpecificDays).GetValue<bool>() && !ShouldGiveToday(med))
                {
                    continue;
                }

                //Specific day of Month
                if(med.HasValue(configuration.MedsDosage_SpecificDayOfMonth))
                {
                    if(!(DateTime.Now.Day == Int32.Parse(med.GetProperty(configuration.MedsDosage_SpecificDayOfMonth).GetValueAsLocalizedText())))
                    {
                        continue;
                    }
                }

                //4 hour cycle
                if (med.HasValue(configuration.MedsDosage_4Hourly) && med.GetProperty(configuration.MedsDosage_4Hourly).GetValue<bool>() && ShouldGiveNow4Hourly(med))
                {
                    medsToGive.Add(lookup);
                    continue;
                }

                //Meds on script timeslot
                if (isPRN)
                {
                    if (med.HasValue(configuration.PRNMedication) && med.GetProperty(configuration.PRNMedication).GetValue<bool>())
                        medsToGive.Add(lookup);
                }
                else
                {
                    if (med.HasValue(slotConfig) && med.GetProperty(slotConfig).GetValue<bool>() && 
                        !(med.HasValue(configuration.PRNMedication) && med.GetProperty(configuration.PRNMedication).GetValue<bool>()))
                    {
                        medsToGive.Add(lookup);
                    }
                }
            }

            return medsToGive;
        }

        private bool ShouldGiveNow4Hourly(ObjVerEx med)
        {
            int giveThreshold = 30;

            string startTime = med.GetProperty(configuration.MedsDosage_StartTimeOf4HourlyCycle).TypedValue.GetValueAsLocalizedText();
            DateTime startDate = DateTime.Parse($"2000-01-01 {startTime}").AddMinutes(-giveThreshold);
            DateTime endDate = startDate.AddMinutes(240 + giveThreshold);
            
            DateTime now = DateTime.Parse($"2000-01-01 {DateTime.Now.ToShortTimeString()}");

            for(int i = 0; i < 6; i++)
            {
                if (now > startDate && now < endDate)
                {
                    return true;
                }

                startDate.AddHours(4);
                endDate.AddHours(4);
            }

            return false;
        }

        private bool ShouldGiveToday(ObjVerEx med)
        {
            foreach (Lookup day in med.GetLookups(configuration.DaysOfWeek))
            {
                if(DayOfWeekParser.isToday(day.DisplayValue))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
