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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ChangePincode")]
        public void ChangeStaffPinCode(EventHandlerEnvironment env)
        {
            var staffLookup = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_StaffName).TypedValue.GetValueAsLookup();
            ObjVerEx staff = new ObjVerEx(env.Vault, staffLookup);

            var currentStaffPinCode = staff.Properties.GetProperty(Configuration.Staff_PinCode);
            var oldPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_OldPinCode);

            //MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            //mFSearchBuilder.Class(Configuration.Staff);
            //mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, oldPinCode.GetValueAsLocalizedText());
            //var staffUserRecord = mFSearchBuilder.FindOneEx();

            if (currentStaffPinCode.GetValueAsLocalizedText() != oldPinCode.GetValueAsLocalizedText())
                throw new Exception($"Incorrect pin(Old Pin)");

            var newPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_NewPinCode);
            var reEnterNewPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_ReEnterNewPinCode);


            if (newPinCode.GetValueAsLocalizedText() != reEnterNewPinCode.GetValueAsLocalizedText())
                throw new Exception($"New pin code and re-entered pin code do not match.");

            MFSearchBuilder existingPincodeSearch = new MFSearchBuilder(env.ObjVerEx.Vault);
            existingPincodeSearch.Class(Configuration.Staff);
            existingPincodeSearch.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(newPinCode.GetValueAsLocalizedText()));
            var existingObjectsWithSamePincode = existingPincodeSearch.FindEx();

            if (existingObjectsWithSamePincode.Count > 1)
                throw new Exception($"Invalid New Pin, please try a different pin");

            staff.SaveProperty(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, newPinCode.GetValueAsLocalizedText());
        }
    }
}
