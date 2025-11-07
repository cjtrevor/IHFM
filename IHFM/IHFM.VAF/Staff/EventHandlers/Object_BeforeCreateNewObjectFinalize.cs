using System;
using MFiles.VAF.Common;
using MFilesAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        //[EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Priority = 100)]
        //public void CheckPinCode(EventHandlerEnvironment env)
        //{
        //    return;
        //    var pinVal = env.ObjVerEx.Properties.GetProperty(Configuration.StaffLookup_StaffPinCode);

        //    if (pinVal == null)
        //        return;

        //    MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
        //    mFSearchBuilder.Class(Configuration.Staff);
        //    mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, pinVal.GetValueAsLocalizedText());
        //    var staffUserObject = mFSearchBuilder.FindOneEx();

        //    if (staffUserObject == null)
        //        throw new Exception($"Incorrect pin");

        //    env.ObjVerEx.SetProperty(Configuration.CreatedBy, MFDataType.MFDatatypeLookup, staffUserObject.ID);
        //}
    }
}
