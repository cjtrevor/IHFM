using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class EventsActivitiesSearchService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public EventsActivitiesSearchService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public List<ObjVerEx> GetResidentEvents(int residentId)
        {
            MFSearchBuilder searchBuilder = new MFSearchBuilder(vault);
            searchBuilder.Class(configuration.Events_ActivitiesEventsClass);
            searchBuilder.Property(configuration.Events_ResidentsDropdown, MFDataType.MFDatatypeMultiSelectLookup, residentId);
 
            return searchBuilder.FindEx();
        }
    }
}
