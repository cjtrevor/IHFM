using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public bool GetResidentOnCarePackage(Lookup residentLookup)
        {
            ObjVerEx resident = new ObjVerEx(_vault, residentLookup);
            
            if(!resident.HasValue(_configuration.OnCarePlan))
            {
                return false;
            }

            return resident.GetProperty(_configuration.OnCarePlan).GetValue<bool>();
        }

        public List<ObjVer> GetResidentTBCItems(Lookup residentLookup)
        {
            List<ObjVer> objVers = new List<ObjVer>();
            ObjVerEx resident = new ObjVerEx(_vault, residentLookup);
            
            //Get Daily Items
            objVers.AddRange(GetTBCItems(resident,_configuration.DailyADLLookup));

            //Get Weekly Items
            objVers.AddRange(GetTBCItems(resident, _configuration.WeekdaysADLLookup));

            //Get Specific Day Items
            objVers.AddRange(GetTBCItems(resident, GetADLAliasForDayOfWeek()));
            
            return objVers;
        }

        private MFIdentifier GetClinicAliasForDayOfWeek()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return _configuration.SundayClinicLookup;
                case DayOfWeek.Monday:
                    return _configuration.MondayClinicLookup;
                case DayOfWeek.Tuesday:
                    return _configuration.TuesdayClinicLookup;
                case DayOfWeek.Wednesday:
                    return _configuration.WednesdayClinicLookup;
                case DayOfWeek.Thursday:
                    return _configuration.ThursdayClinicLookup;
                case DayOfWeek.Friday:
                    return _configuration.FridayClinicLookup;
                case DayOfWeek.Saturday:
                    return _configuration.SaturdayClinicLookup;
                default:
                    return _configuration.SundayClinicLookup;
            }
        }

        private MFIdentifier GetADLAliasForDayOfWeek()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return _configuration.SundayADLLookup;
                case DayOfWeek.Monday:
                    return _configuration.MondayADLLookup;
                case DayOfWeek.Tuesday:
                    return _configuration.TuesdayADLLookup;
                case DayOfWeek.Wednesday:
                    return _configuration.WednesdayADLLookup;
                case DayOfWeek.Thursday:
                    return _configuration.ThursdayADLLookup;
                case DayOfWeek.Friday:
                    return _configuration.FridayADLLookup;
                case DayOfWeek.Saturday:
                    return _configuration.SaturdayADLLookup;
                default:
                    return _configuration.SundayADLLookup;
            }
        }

        private List<ObjVer> GetTBCItems(ObjVerEx resident, MFIdentifier alias)
        {
            List<ObjVer> objVers = new List<ObjVer>();

            Lookups tbcScheduleItems = resident.GetLookups(alias);

            foreach (Lookup item in tbcScheduleItems)
            {
                ObjVerEx scheduleItem = new ObjVerEx(_vault, item);

                Lookups times = scheduleItem.GetLookups(_configuration.TBCS_TbcScheduledTimes);

                foreach(Lookup time in times)
                {
                    if(ScheduledItemIsInCurrentTimeSlot(time.DisplayValue))
                    {
                        Lookup tbcItem = scheduleItem.GetProperty(_configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
                        objVers.Add(tbcItem.GetAsObjVer());
                    }
                }           
            }

            return objVers;
        }

        private bool ScheduledItemIsInCurrentTimeSlot(string time)
        {
            string today = DateTime.Today.ToString("dd-MM-yyyy");

            DateTime timeslot = DateTime.ParseExact($"{today} {time}", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            DateTime startSlot = timeslot.AddHours(-1);
            DateTime endSlot = timeslot.AddHours(1);

            return DateTime.Compare(DateTime.Now, startSlot) > 0 && DateTime.Compare(DateTime.Now,endSlot) < 0;
        }

        public List<ObjVer> GetResidentTBCClinicItems(Lookup residentLookup)
        {
            List<ObjVer> objVers = new List<ObjVer>();
            ObjVerEx resident = new ObjVerEx(_vault, residentLookup);

            //Get Daily Items
            objVers.AddRange(GetTBCItems(resident, _configuration.DailyClinicLookup));

            //Get Weekly Items
            objVers.AddRange(GetTBCItems(resident, _configuration.WeekdaysClinicLookup));

            //Get Specific Day Items
            objVers.AddRange(GetTBCItems(resident, GetClinicAliasForDayOfWeek()));

            return objVers;
        }

        public void SetCarePlanFlag(Lookup residentLookup)
        {
            ObjVerEx resident = new ObjVerEx(_vault, residentLookup);
            resident.SaveProperty(_configuration.HasCarePlan, MFDataType.MFDatatypeBoolean, true);
        }

        public void SetNoBowelMovementCount(ObjVerEx resident, bool reset)
        {
            int currentCount = resident.HasValue(_configuration.Resident_NoBowelCount) ? resident.GetProperty(_configuration.Resident_NoBowelCount).GetValue<int>() : 0;
            resident.SaveProperty(_configuration.Resident_NoBowelCount, MFDataType.MFDatatypeInteger, reset ? 0 : currentCount + 1);
        }

        public void SetNoEatCount(ObjVerEx resident, bool reset)
        {
            int currentCount = resident.HasValue(_configuration.Resident_NoEatCount) ? resident.GetProperty(_configuration.Resident_NoEatCount).GetValue<int>() : 0;
            resident.SaveProperty(_configuration.Resident_NoEatCount, MFDataType.MFDatatypeInteger, reset ? 0 : currentCount + 1);
        }

        public void SetNoBathCount(ObjVerEx resident, bool reset)
        {
            int currentCount = resident.HasValue(_configuration.Resident_NoBathCount) ? resident.GetProperty(_configuration.Resident_NoBathCount).GetValue<int>() : 0;
            resident.SaveProperty(_configuration.Resident_NoBathCount, MFDataType.MFDatatypeInteger, reset ? 0 : currentCount + 1);
        }
    }
}
