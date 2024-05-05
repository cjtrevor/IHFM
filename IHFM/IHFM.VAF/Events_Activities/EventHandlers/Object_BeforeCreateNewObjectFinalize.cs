using MFilesAPI;
using MFiles.VAF.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.AttendanceFeedback")]
        public void AfterCreateNewAttendanceFeedback(EventHandlerEnvironment env)
        {
            Lookup eventLookup = env.ObjVerEx.GetProperty(Configuration.Attendance_WhichEvent).TypedValue.GetValueAsLookup();
            ObjVerEx eventObj = new ObjVerEx(env.Vault, eventLookup);

            Lookups residents = eventObj.GetLookups(Configuration.Attendance_ResidentsDropdown);
            string date = eventObj.GetProperty(Configuration.Attendance_Date).TypedValue.GetValueAsLocalizedText();

            foreach(Lookup item in residents)
            {
                env.ObjVerEx.AddLookup(Configuration.Attendance_ResidentsDropdown, item.ToLatestObjVer(env.Vault));
            }

            //env.ObjVerEx.SetProperty(Configuration.Attendance_Date, MFDataType.MFDatatypeDate, DateTime.Parse(date));

        }
    }
}
