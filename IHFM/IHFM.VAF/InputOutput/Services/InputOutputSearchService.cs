using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class InputOutputSearchService
    {
        private Vault _vault;
        private Configuration _configuration;

        public InputOutputSearchService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public ObjVerEx FindInputOutputTotalByShiftResident(string shiftNumber, int residentLookupID)
        {
            MFSearchBuilder mfIOSearch = new MFSearchBuilder(_vault);
            mfIOSearch.Class(_configuration.IntakeOutputTotalClass);
            mfIOSearch.Property(_configuration.ShiftIO,MFDataType.MFDatatypeText, shiftNumber);
            mfIOSearch.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentLookupID);
            mfIOSearch.Deleted(false);

            ObjectSearchResults objectSearchResults = mfIOSearch.Find();
            if (objectSearchResults.Count == 0)
                return null;
            else
                return mfIOSearch.FindOneEx();
        }
    }
}
