using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Priority = 100)]
        public void CheckPinCode(EventHandlerEnvironment env)
        {
            var pinVal = env.ObjVerEx.Properties.GetProperty(Configuration.StaffLookup_StaffPinCode);

            if (pinVal == null)
                return;

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(pinVal.GetValueAsLocalizedText()));
            var staffUserObject = mFSearchBuilder.FindOneEx();

            if (staffUserObject == null)
                throw new Exception($"Incorrect pin");

            env.ObjVerEx.SetProperty(Configuration.CreatedBy, MFDataType.MFDatatypeLookup, staffUserObject.ID);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.Staff")]
        public void BeforeCheckInChanges_EnsureUniqueStaffPinCode(EventHandlerEnvironment env)
        {
            var staffPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.Staff_PinCode);

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(staffPinCode.GetValueAsLocalizedText()));
            var existingObjectsWithSamePincode = mFSearchBuilder.FindEx();

            if (existingObjectsWithSamePincode.Count > 1)
                throw new Exception($"Invalid pin, please try a different pin");
        }

    }
}
