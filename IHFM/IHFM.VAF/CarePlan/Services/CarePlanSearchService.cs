using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class CarePlanSearchService
    {
        private Vault _vault;
        private Configuration _configuration;

        public CarePlanSearchService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public ObjVerEx GetResidentCarePlan(int residentId)
        {
            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.ObjType(_configuration.CarePlanObject);
            search.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            if(search.Find().Count > 1)
            {
                return search.FindOneEx();
            }

            return null;
        }

        public ObjVerEx GetResidentCarePlanExisting(int residentId)
        {
            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.ObjType(_configuration.CarePlanObject);
            search.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            if (search.Find().Count >= 1)
            {
                return search.FindOneEx();
            }

            return null;
        }
    }
}
