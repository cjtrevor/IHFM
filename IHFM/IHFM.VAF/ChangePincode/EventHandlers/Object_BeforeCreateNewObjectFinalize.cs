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
            var oldPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.ChangePincode_OldPinCode);

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(oldPinCode.GetValueAsLocalizedText()));
            var staffUserRecord = mFSearchBuilder.FindOneEx();

            if (staffUserRecord == null)
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


            staffUserRecord.SaveProperty(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(newPinCode.GetValueAsLocalizedText()));
        }
    }
}
