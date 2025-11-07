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
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ChangePincode")]
        //public void ChangeStaffPinCode(EventHandlerEnvironment env)
        //{
        //    var oldPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_OldPinCode);

        //    MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
        //    mFSearchBuilder.Class(Configuration.Staff);
        //    mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, oldPinCode.GetValueAsLocalizedText());
        //    var staffUserRecord = mFSearchBuilder.FindOneEx();

        //    if (staffUserRecord == null)
        //        throw new Exception($"Incorrect pin(Old Pin)");


        //    var newPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_NewPinCode);
        //    var reEnterNewPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_ReEnterNewPinCode);


        //    if (newPinCode.GetValueAsLocalizedText() != reEnterNewPinCode.GetValueAsLocalizedText())
        //        throw new Exception($"New pin code and re-entered pin code do not match.");


        //    staffUserRecord.SaveProperty(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, newPinCode.GetValueAsLocalizedText());
        //}
    }
}
