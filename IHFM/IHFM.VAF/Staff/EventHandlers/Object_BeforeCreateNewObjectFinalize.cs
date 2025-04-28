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

            //var hasCreatedByPropertyOnClass = env.ObjVerEx.HasProperty(Configuration.CreatedBy);

            if (pinVal == null)
                return;

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_PinCode, MFDataType.MFDatatypeText, pinVal.GetValueAsLocalizedText());
            var staffUserObject = mFSearchBuilder.FindOneEx();

            if (staffUserObject == null)
                throw new Exception($"incorrect pin");

            env.ObjVerEx.SetProperty(Configuration.CreatedBy, MFDataType.MFDatatypeLookup, staffUserObject.ID);
        }
    }
}
