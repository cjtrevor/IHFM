using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ScheduledTimeBasedCareService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public ScheduledTimeBasedCareService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public List<Lookup> GetScheduledTimeBasedCareForResident(ObjVerEx resident)
        {
            List<Lookup> items = new List<Lookup>();

            //Get Everyday
            //Get For specific day

            return items;
        }

        
    }
}
