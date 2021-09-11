using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class AgeBackgroundOperations
    {
        public void RefreshResidentAge(Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService();
            AgeCalculationService ageCalculationService = new AgeCalculationService();

            List<ObjVerEx> residents = residentSearchService.GetAllResidents(vault, configuration);
            residents.ForEach(x => {
                ageCalculationService.RefreshAge(x, configuration);
            });
        }
    }
}
