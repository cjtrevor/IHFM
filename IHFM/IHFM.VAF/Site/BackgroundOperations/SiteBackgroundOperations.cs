using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class SiteBackgroundOperations
    {
        public void SetSiteNominals(Vault vault, Configuration configuration)
        {
            SiteSearchService siteServ = new SiteSearchService(vault, configuration);
            List<ObjVerEx> allSites = siteServ.GetAllSites();

            ResidentSearchService resServ = new ResidentSearchService(vault, configuration);
            RoomSearchService roomServ = new RoomSearchService(configuration, vault);

            List<int> validZones = new List<int>
            {
                configuration.Zone_FrailCareItem.ID,
                configuration.Zone_MemoryCareItem.ID,
                configuration.Zone_SickBeds.ID
            };

            foreach(ObjVerEx site in allSites)
            {
                List<ObjVerEx> rooms = roomServ.GetRoomsByZone(site.ID, validZones);
                int vacantBeds = 0;
                int occupiedBeds = 0;
                int totalBeds = 0;
                double fullIncome = 0;
                double actualIncome = 0;
                double variance = 0;
                string lastUpdated = "";

                rooms.ForEach(x =>
                {
                    double tarriff;
                    if(x.HasValue(configuration.Vacant) && x.GetProperty(configuration.Vacant).GetValue<bool>())
                    {
                        vacantBeds++;
                    }
                    else
                    {
                        occupiedBeds++;
                    }

                    if(double.TryParse(x.GetProperty(configuration.RoomTariff).GetValueAsLocalizedText(),out tarriff))
                    {
                        fullIncome += tarriff; 
                    }
                    else
                    {
                        //Invalid Tarriff
                        SysUtils.ReportInfoToEventLog($"IHFM: SetSiteNominals - Room has invalid tarriff (ID: {x.ID})");
                    }
                });
                totalBeds = vacantBeds + occupiedBeds;

                List<ObjVerEx> residents = resServ.GetResidentsBySiteAndZone(site.ID, validZones);

                residents.ForEach(x =>
                {
                    double actualPaying;
                    if(double.TryParse(x.GetProperty(configuration.Resident_ActualAmountPayable).GetValueAsLocalizedText(),out actualPaying))
                    {
                        actualIncome += actualPaying;
                    }
                    else
                    {
                        //Invalid Actual Paying
                        SysUtils.ReportInfoToEventLog($"IHFM: SetSiteNominals - Resident has invalid actual paying (ID: {x.ID})");
                    }
                });

                variance = fullIncome - actualIncome;
                lastUpdated = DateTime.Now.ToString("ddMMMyyyy hh:mm");

                //Update properties
                site.SetProperty(configuration.Site_ActualIncome, MFDataType.MFDatatypeFloating, actualIncome);
                site.SetProperty(configuration.Site_FullIncome, MFDataType.MFDatatypeFloating, fullIncome);
                site.SetProperty(configuration.Site_Variance, MFDataType.MFDatatypeFloating, variance);
                site.SetProperty(configuration.Site_VacantBeds, MFDataType.MFDatatypeFloating, vacantBeds);
                site.SetProperty(configuration.Site_OccupiedBeds, MFDataType.MFDatatypeFloating, occupiedBeds);
                site.SetProperty(configuration.Site_TotalBeds, MFDataType.MFDatatypeFloating, totalBeds);
                site.SetProperty(configuration.Site_LastDataUpdate, MFDataType.MFDatatypeText, lastUpdated);
                site.SaveProperties();
            }
        }
    }
}
