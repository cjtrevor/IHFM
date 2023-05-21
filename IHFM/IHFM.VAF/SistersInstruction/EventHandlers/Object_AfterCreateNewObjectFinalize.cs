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
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.SistersInstructionActivity")]
        public void AfterCreateNewSistersInstructionActivityChange(EventHandlerEnvironment env)
        {
            SetLastActionedDateForParentRecord(env);
        }

        private void SetLastActionedDateForParentRecord(EventHandlerEnvironment env)
        {
            Lookup lookupParent = env.ObjVerEx.GetProperty(Configuration.SistersInstructionActivity_SistersIntructionDropdown).TypedValue.GetValueAsLookup();

            ObjVerEx parent = new ObjVerEx(env.Vault, lookupParent);

            parent.SetProperty(Configuration.SistersInstruction_DateLastActioned, MFDataType.MFDatatypeDate, DateTime.Now);
            parent.SetProperty(Configuration.SistersInstruction_TimeLastActioned, MFDataType.MFDatatypeTime, DateTime.Now);

            parent.SaveProperties(); 
        }
    }
}
