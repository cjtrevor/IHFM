using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ResidentPropertyService
    {
        private Vault _vault;
        private Configuration _configuration;
        public ResidentPropertyService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public List<ObjVer> GetResidentTBCItems(Lookup residentLookup)
        {
            List<ObjVer> objVers = new List<ObjVer>();

            ObjVerEx objVerEx = new ObjVerEx(_vault, residentLookup);
            Lookups tbcItems = objVerEx.GetLookups(_configuration.TBCADLLookup);

            foreach (Lookup item in tbcItems)
            {
                objVers.Add(item.GetAsObjVer());
            }

            return objVers;
        }

        public List<ObjVer> GetResidentTBCClinicItems(Lookup residentLookup)
        {
            List<ObjVer> objVers = new List<ObjVer>();

            ObjVerEx objVerEx = new ObjVerEx(_vault, residentLookup);
            Lookups tbcItems = objVerEx.GetLookups(_configuration.TBCClinicLookup);

            foreach (Lookup item in tbcItems)
            {
                objVers.Add(item.GetAsObjVer());
            }

            return objVers;
        }
    }
}
