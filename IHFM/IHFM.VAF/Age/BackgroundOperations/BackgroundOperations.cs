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

        public void SetAverageSiteAges(Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService();
            SiteSearchService siteSearchService = new SiteSearchService(vault,configuration);

            List<ObjVerEx> sites = siteSearchService.GetAllSites();
            List<ObjVerEx> residents = residentSearchService.GetAllActiveResidents(vault, configuration);

            foreach (ObjVerEx site in sites)
            {
                int baseSiteID = site.GetLookupID(configuration.BaseSiteID);

                List<ObjVerEx> siteResidents = residents.Where(x => x.GetLookupID(configuration.BaseSiteID) == baseSiteID).ToList();
                int noOfResidents = siteResidents.Count;
                int totalResidentAge = 0;

                foreach (ObjVerEx resident in siteResidents)
                {
                    int age = GetResidentAge(resident, configuration);

                    if(age == -1)
                    {
                        noOfResidents--;
                        continue;
                    }

                    totalResidentAge += age;
                }

                if(noOfResidents > 0)
                {
                    int averageAge = totalResidentAge / noOfResidents;
                    site.SetProperty(configuration.AverageSiteAge, MFDataType.MFDatatypeInteger, averageAge);
                    site.SetProperty(configuration.NumOfResidents, MFDataType.MFDatatypeInteger, noOfResidents);
                    site.SaveProperties();
                }
            }
        }

        private int GetResidentAge(ObjVerEx resident, Configuration configuration)
        {
            string age = resident.GetPropertyText(configuration.Age);
            int ageInt;

            if(!Int32.TryParse(age,out ageInt))
            {
                return -1;
            }

            return ageInt;
        }
    }
}
