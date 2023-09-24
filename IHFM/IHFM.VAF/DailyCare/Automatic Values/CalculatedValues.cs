using MFiles.VAF.Common;
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
        [PropertyCustomValue("MFiles.Property.EventsactivitiesNotes")]
        public TypedValue SetEventsActivitiesNotesValue(PropertyEnvironment env)
        {
            string output = "";
            int residentId = env.ObjVerEx.GetLookupID(Configuration.DailyCare_Resident);

            //Get Resident Activities
            EventsActivitiesSearchService service = new EventsActivitiesSearchService(env.Vault, Configuration);
            List<ObjVerEx> activities = service.GetResidentEvents(residentId);

            activities.ForEach(x => {
                //Check if once off
                if(x.HasValue(Configuration.Events_OnceOnly) && (x.HasValue(Configuration.Events_Date))
                && x.GetProperty(Configuration.Events_OnceOnly).GetValue<bool>())
                {
                    DateTime eventDate = x.GetProperty(Configuration.Events_Date).GetValue<DateTime>();

                    if(eventDate == DateTime.Today)
                    {
                        output += $"{x.GetPropertyText(Configuration.Events_EventSchedule)}{Environment.NewLine}";
                    }
                }
                //Check if daily
                else if (x.HasValue(Configuration.Events_Daily)
                && x.GetProperty(Configuration.Events_Daily).GetValue<bool>())
                {
                    output += $"{x.GetPropertyText(Configuration.Events_EventSchedule)}{Environment.NewLine}";
                }
                //Check weekdays
                else if (x.HasValue(Configuration.Events_Weekdays))
                {
                    string selectedDays = x.GetProperty(Configuration.Events_Weekdays).GetValueAsLocalizedText();
                    string[] values = selectedDays.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if(values.Any(y => y.ToLower() == DateTime.Now.ToString("dddd").ToLower()))
                    {
                        output += $"{x.GetPropertyText(Configuration.Events_EventSchedule)}{Environment.NewLine}";
                    }
                }
                //Check day of month
                else if (x.HasValue(Configuration.Events_Month))
                {
                    string selectedDays = x.GetProperty(Configuration.Events_Month).GetValueAsLocalizedText();
                    string[] values = selectedDays.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if (values.Any(y => y == DateTime.Now.Day.ToString()))
                    {
                        output += $"{x.GetPropertyText(Configuration.Events_EventSchedule)}{Environment.NewLine}";
                    }
                }
            });

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, output);

            return calculated;
        }
    }
}
