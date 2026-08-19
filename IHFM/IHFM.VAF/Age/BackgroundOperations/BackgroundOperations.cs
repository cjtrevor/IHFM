using IHFM.VAF.Base;
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
        public void UpdateResidentDurationProperties(Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService(vault, configuration);

            DateTime now = DateTime.Now;
            DateTime durationLastUpdatedDate = new DateTime(now.Year, now.Month, 2).AddMonths(-1);

            var batchValue = BatchProcessingHelper.GetNextBatchValue("UpdateResidentDurationProperties");
            var residentsToUpdate = residentSearchService.GetAllResidentsBeforeDurationLastUpdatedDateByBatch(durationLastUpdatedDate, batchValue);

            foreach (var resident in residentsToUpdate)
            {
                if (resident.HasValue(configuration.Resident_DateAdmittedToFrailCare))
                {
                    DateTime admissionDate_FrailCare = DateTime.Parse(resident.GetProperty(configuration.Resident_DateAdmittedToFrailCare).GetValueAsLocalizedText());
                    var days_FrailCare = (now - admissionDate_FrailCare).Days;

                    resident.SetProperty(configuration.Resident_DurationOfStayInFrailcare, MFDataType.MFDatatypeInteger, days_FrailCare);
                }
                else if (resident.HasValue(configuration.Resident_DateAdmittedToFacility))
                {
                    DateTime admissionDate_Facility_Fallback = DateTime.Parse(resident.GetProperty(configuration.Resident_DateAdmittedToFacility).GetValueAsLocalizedText());
                    var days_FrailCare_Fallback = (now - admissionDate_Facility_Fallback).Days;

                    resident.SetProperty(configuration.Resident_DurationOfStayInFrailcare, MFDataType.MFDatatypeInteger, days_FrailCare_Fallback);
                }

                if (resident.HasValue(configuration.Resident_DateAdmittedToFacility))
                {
                    DateTime admissionDate_Facility = DateTime.Parse(resident.GetProperty(configuration.Resident_DateAdmittedToFacility).GetValueAsLocalizedText());
                    var days_Facility = (now - admissionDate_Facility).Days;

                    resident.SetProperty(configuration.Resident_DurationOfStayInFacility, MFDataType.MFDatatypeInteger, days_Facility);
                }
                   
                resident.SetProperty(configuration.Resident_DurationsLastUpdated, MFDataType.MFDatatypeTimestamp, DateTime.Now);
                resident.SaveProperties();
            }
        }

        public void RefreshResidentAge(Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService(vault,configuration);
            AgeCalculationService ageCalculationService = new AgeCalculationService();

            List<ObjVerEx> residents = residentSearchService.GetAllResidentsWithDobToday();
            residents.ForEach(x => {
                ageCalculationService.RefreshAge(x, configuration);
            });
        }

        public void SetAverageSiteAges(Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService(vault, configuration);
            SiteSearchService siteSearchService = new SiteSearchService(vault,configuration);

            List<ObjVerEx> sites = siteSearchService.GetAllSites();
            List<ObjVerEx> residents = residentSearchService.GetAllActiveResidents();

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
