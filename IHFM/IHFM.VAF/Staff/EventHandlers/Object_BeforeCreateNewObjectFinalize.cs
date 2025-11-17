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

            if (string.IsNullOrEmpty(pinVal.GetValueAsLocalizedText()))
                throw new Exception($"Staff Pin Code required");

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeInteger, int.Parse(pinVal.GetValueAsLocalizedText()));
            var staffUserObject = mFSearchBuilder.FindOneEx();

            if (staffUserObject == null)
                throw new Exception($"Incorrect pin");

            env.ObjVerEx.SetProperty(Configuration.CreatedBy, MFDataType.MFDatatypeLookup, staffUserObject.ID);
        }
    }
}
