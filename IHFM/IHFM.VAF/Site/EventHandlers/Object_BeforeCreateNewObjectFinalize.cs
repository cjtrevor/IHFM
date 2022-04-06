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
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.StockIssue")]
        public void SetStockIssueSite(EventHandlerEnvironment env)
        {
            SitePermissionService sitePermissionService = new SitePermissionService(env.Vault, Configuration);
            sitePermissionService.SetSiteFromStaffByUserID(env.ObjVerEx, Configuration.VAFSite);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.OtherDocument")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.Document")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MenuWeekly")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.EmergencyLightChecklist")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.InspectionFacilityCare")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.InspectionHealthCare")]

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.GuardhouseHSChecklist")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.GuardhouseHandover")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.HealthcareShiftReport")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.PatrolChecklist")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.ReceptionShiftReport")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.RiskmanChecklist")]
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.RiskmanShiftReport")]

        public void SetSiteIDForOtherDocuments(EventHandlerEnvironment env)
        {
            SitePermissionService sitePermissionService = new SitePermissionService(env.Vault, Configuration);
            sitePermissionService.SetSiteFromStaffByUserID(env.ObjVerEx, Configuration.VAFSite);
        }
    }
}
