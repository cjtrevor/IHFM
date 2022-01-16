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

        public List<Lookup> GetMedsScheduledForTimeSlot(MFIdentifier slotConfig, ObjVerEx script)
        {
            List<Lookup> medsToGive = new List<Lookup>();
            Lookups meds = script.GetLookups(configuration.MedsOnScript);

            foreach  (Lookup lookup in meds)
            {
                ObjVerEx med = new ObjVerEx(vault, lookup);
                if(med.HasValue(slotConfig) && med.GetProperty(slotConfig).GetValue<bool>())
                {
                    medsToGive.Add(lookup);
                }
            }

            return medsToGive;
        }
    }
}
