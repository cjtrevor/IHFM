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

                if(med.HasValue(configuration.SpecificDays) && med.GetProperty(configuration.SpecificDays).GetValue<bool>() && !ShouldGiveToday(med))
                {
                    continue;
                }

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
