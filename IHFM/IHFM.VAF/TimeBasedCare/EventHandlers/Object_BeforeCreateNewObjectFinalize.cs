using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        private bool ShouldAddStartTime(EventHandlerEnvironment env)
        {
            if (env.ObjVerEx.HasValue(Configuration.OverrideStartTime) &&
                env.ObjVerEx.GetProperty(Configuration.OverrideStartTime).GetValue<bool>())
            {
                return false;
            }

            return true;
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize,Class = "MFiles.Class.TBC")]
        public void SetDefaults(EventHandlerEnvironment env)
        {
            try
            {          
                ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);

                //Start Time
                if(ShouldAddStartTime(env))
                { 
                    env.ObjVerEx.SetProperty(Configuration.StartTimeTBC, MFilesAPI.MFDataType.MFDatatypeTime, DateTime.Now);
                }

                //TBC Items
                Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
                List<ObjVer> TBCADL = residentPropertyService.GetResidentTBCItems(residentLookup);

                TBCADL.ForEach(x => {
                    env.ObjVerEx.AddLookup(Configuration.TBCADLLookup, x);
                });

                //On Care Package
                env.ObjVerEx.SetProperty(Configuration.OnCarePlan, MFDataType.MFDatatypeBoolean, residentPropertyService.GetResidentOnCarePackage(residentLookup));

                env.ObjVerEx.SaveProperties();
            }
            catch(Exception ex)
            {
                throw new Exception("TBC - Set Defaults - " + ex.Message);
            }
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.TBCClinic")]
        public void SetTBCClinicDefaults(EventHandlerEnvironment env)
        {
            ResidentPropertyService residentPropertyService = new ResidentPropertyService(env.Vault, Configuration);
            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();

            //Start Time
            env.ObjVerEx.SetProperty(Configuration.StartTimeTBC, MFilesAPI.MFDataType.MFDatatypeTime, DateTime.Now);

            //TBC Clinic Items
            List<ObjVer> TBCClinic = residentPropertyService.GetResidentTBCClinicItems(residentLookup);

            TBCClinic.ForEach(x => {
                env.ObjVerEx.AddLookup(Configuration.TBCClinicLookup, x);
            });

            //On Care Package
            env.ObjVerEx.SetProperty(Configuration.OnCarePlan, MFDataType.MFDatatypeBoolean, residentPropertyService.GetResidentOnCarePackage(residentLookup));

            env.ObjVerEx.SaveProperties();
        }
    }
}
