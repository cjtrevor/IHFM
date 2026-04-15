using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ChangePincode")]
        public void ChangeStaffPinCode(EventHandlerEnvironment env)
        {
            var staffLookup = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_StaffName).TypedValue.GetValueAsLookup();
            ObjVerEx staff = new ObjVerEx(env.Vault, staffLookup);

            var currentStaffPinCode = staff.Properties.GetProperty(Configuration.Staff_Password);
            var oldPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_OldPassword);

            if (currentStaffPinCode.GetValueAsLocalizedText() != oldPinCode.GetValueAsLocalizedText())
                throw new Exception($"Incorrect password(Old password)");


            var newPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_NewPassword);
            var reEnterNewPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_ReEnterNewPassword);

            if (newPinCode.GetValueAsLocalizedText() != reEnterNewPinCode.GetValueAsLocalizedText())
                throw new Exception($"New password and re-entered password do not match.");

            MFSearchBuilder existingPincodeSearch = new MFSearchBuilder(env.ObjVerEx.Vault);
            existingPincodeSearch.Class(Configuration.Staff);
            existingPincodeSearch.Property(Configuration.Staff_Password, MFDataType.MFDatatypeText, newPinCode.GetValueAsLocalizedText());
            var existingObjectsWithSamePincode = existingPincodeSearch.FindEx();

            if (existingObjectsWithSamePincode.Count > 1)
                throw new Exception($"Invalid New password, please try a different password");


            staff.SaveProperty(Configuration.Staff_Password, MFDataType.MFDatatypeText, newPinCode.GetValueAsLocalizedText());
        }
    }
}
