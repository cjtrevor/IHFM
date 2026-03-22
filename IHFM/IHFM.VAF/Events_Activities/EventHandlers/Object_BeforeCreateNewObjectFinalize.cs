using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.ActivitieseventSchedule")]
        public void AfterChangeActivitiesEventsSchedule(EventHandlerEnvironment env)
        {
            var residents = env.ObjVerEx.GetLookups(Configuration.Events_ResidentsDropdown);
            CarePlanSearchService searchService = new CarePlanSearchService(env.Vault, Configuration);

            foreach (Lookup resident in residents)
            {
                ObjVerEx careplan = searchService.GetResidentCarePlanExisting(resident.Item);
                if (careplan == null)
                    continue;

                MFIdentifier careplanMFLookup;

                if (env.ObjVerEx.HasValue(Configuration.Events_OnceOnly) && (env.ObjVerEx.HasValue(Configuration.Events_Date))
                && env.ObjVerEx.GetProperty(Configuration.Events_OnceOnly).GetValue<bool>())
                {
                    careplanMFLookup = Configuration.Careplan_EventsActivitiesOnceOff;
                }
                else if (env.ObjVerEx.HasValue(Configuration.Events_Daily)
                && env.ObjVerEx.GetProperty(Configuration.Events_Daily).GetValue<bool>())
                {
                    careplanMFLookup = Configuration.Careplan_EventsActivitiesDaily;
                }
                else if (env.ObjVerEx.HasValue(Configuration.Events_Weekdays))
                {
                    careplanMFLookup = Configuration.Careplan_EventsActivitiesWeekly;
                }
                else if (env.ObjVerEx.HasValue(Configuration.Events_WeeksOfMonth) || env.ObjVerEx.HasValue(Configuration.Events_Month))
                {
                    careplanMFLookup = Configuration.Careplan_EventsActivitiesMonthly;
                }
                else
                {
                    continue;
                }

                var activityCompleted = env.ObjVerEx.GetPropertyAsBoolean(Configuration.DailyCare_IsComplete) ?? false;
                if (activityCompleted)
                {
                    careplan.RemoveLookup(careplanMFLookup, env.ObjVerEx.ID);
                }
                else
                {
                    careplan.AddLookup(careplanMFLookup, env.ObjVerEx.ID);
                }
                careplan.SaveProperties();
            }
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.AttendanceFeedback")]
        public void AfterCreateNewAttendanceFeedback(EventHandlerEnvironment env)
        {
            Lookup eventLookup = env.ObjVerEx.GetProperty(Configuration.Attendance_WhichEvent).TypedValue.GetValueAsLookup();
            ObjVerEx eventObj = new ObjVerEx(env.Vault, eventLookup);

            Lookups residents = eventObj.GetLookups(Configuration.Attendance_ResidentsDropdown);
            string date = eventObj.GetProperty(Configuration.Attendance_Date).TypedValue.GetValueAsLocalizedText();

            foreach (Lookup item in residents)
            {
                env.ObjVerEx.AddLookup(Configuration.Attendance_ResidentsDropdown, item.ToLatestObjVer(env.Vault));
            }

            //env.ObjVerEx.SetProperty(Configuration.Attendance_Date, MFDataType.MFDatatypeDate, DateTime.Parse(date));

        }
    }
}
