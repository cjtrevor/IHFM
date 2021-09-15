using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize,Class = "MFiles.Class.TBC")]
        public void SetDefaults(EventHandlerEnvironment env)
        {
            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);

            //Start Time
            env.ObjVerEx.SetProperty(Configuration.StartTimeTBC, MFilesAPI.MFDataType.MFDatatypeTime, DateTime.Now);
            
            //TBC Items
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            List<ObjVer> TBCADL = residentPropertyService.GetResidentTBCItems(residentLookup);

            TBCADL.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCADLLookup, x);
            });

            env.ObjVerEx.SaveProperties();
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.TBCClinic")]
        public void SetTBCClinicDefaults(EventHandlerEnvironment env)
        {
            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();

            //TBC Clinic Items
            List<ObjVer> TBCClinic = residentPropertyService.GetResidentTBCClinicItems(residentLookup);

            TBCClinic.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCClinicLookup, x);
            });

            env.ObjVerEx.SaveProperties();
        }
    }
}
