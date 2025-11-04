using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ShiftAllocation")]
        public void BeforeCreateNewShiftAllocation(EventHandlerEnvironment env)
        {
            var shiftAllocationDatePart = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_StartDate);
            var shiftAllocationTimePart = env.ObjVerEx.GetPropertyText(Configuration.ShiftAllocation_Time);
            DateTime selectedDate = DateTime.Parse($"{shiftAllocationDatePart} {shiftAllocationTimePart}");

            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);
            
            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);
            ObjVerEx careplan = searchService.GetResidentCarePlanExisting(residentLookup.Item);

            List<ObjVer> objVers = new List<ObjVer>();

            objVers.AddRange(GetTBCSItemsByResident(resident, Configuration.DailyADLLookup));
            //objVers.AddRange(GetTBCSItemsByResident(resident, Configuration.WeekdaysADLLookup));
            objVers.AddRange(GetTBCSItemsByResident(resident, GetADLAliasForDayOfWeek(selectedDate)));

            int totalTimeInMinutes = 0;

            foreach (ObjVer item in objVers)
            {
                var objVerEx = new ObjVerEx(env.Vault, item);

                var timeBasedCareItemLookup = objVerEx.GetProperty(Configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
                ObjVer timeBasedCareItem = timeBasedCareItemLookup.GetAsObjVer();
                var timeBasedCareItemVerEx = new ObjVerEx(env.Vault, timeBasedCareItem);

                Int32.TryParse(timeBasedCareItemVerEx.GetPropertyText(Configuration.AverageTime), out int time);
                totalTimeInMinutes += time;
            }

            var calculatedEndDate = selectedDate.AddMinutes(totalTimeInMinutes);
            env.ObjVerEx.SetProperty(Configuration.ShiftAllocation_EndDate, MFDataType.MFDatatypeText, calculatedEndDate.ToString());

            env.ObjVerEx.SaveProperties();
        }



        private List<ObjVer> GetTBCSItemsByResident(ObjVerEx resident, MFIdentifier alias)
        {
            List<ObjVer> objVers = new List<ObjVer>();

            Lookups tbcScheduleItems = resident.GetLookups(alias);

            foreach (Lookup item in tbcScheduleItems)
            {
                objVers.Add(item.GetAsObjVer());
            }

            return objVers;
        }

        private MFIdentifier GetADLAliasForDayOfWeek(DateTime dateToCheck)
        {

            var dayOfWeek = dateToCheck.DayOfWeek;

            switch (dateToCheck.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return Configuration.SundayADLLookup;
                case DayOfWeek.Monday:
                    return Configuration.MondayADLLookup;
                case DayOfWeek.Tuesday:
                    return Configuration.TuesdayADLLookup;
                case DayOfWeek.Wednesday:
                    return Configuration.WednesdayADLLookup;
                case DayOfWeek.Thursday:
                    return Configuration.ThursdayADLLookup;
                case DayOfWeek.Friday:
                    return Configuration.FridayADLLookup;
                case DayOfWeek.Saturday:
                    return Configuration.SaturdayADLLookup;
                default:
                    return Configuration.SundayADLLookup;
            }
        }

    }
}
