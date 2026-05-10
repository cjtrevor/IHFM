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
        //[EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.EnquirySupportDocuments")]
        public void EnquirySupportDocuments_AfterCheckInChangesFinalize(EventHandlerEnvironment env)
        {
            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            foreach (PropertyValueChange change in changes.Changed)
            {
                if (change.PropertyDef == Configuration.EnquirySupportDocuments_ExistingClient.ID && change.ChangeType == PropertyValueChangeType.Modified))
                {
                    //Might need to handle this scenario, leaving logic here for now
                    //Can move EnquirySupportDocuments_AfterCreateNewObjectFinalize to service and call it from both places if needed
                }
            }
        }
    }
}
