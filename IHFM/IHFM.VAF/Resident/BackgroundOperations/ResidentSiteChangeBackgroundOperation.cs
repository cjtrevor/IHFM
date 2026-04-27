using IHFM.VAF.Resident.Services;
using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF.Resident.BackgroundOperations
{
    /// <summary>
    /// Processes one pending Resident site change queue item per run,
    /// updating the Site property on the related object.
    /// </summary>
    public class ResidentSiteChangeBackgroundOperation
    {
        public void ProcessNextQueueItem(Vault vault, Configuration configuration)
        {
            var item = ResidentSiteChangeQueueService.Dequeue();
            if (item == null)
                return;

            try
            {
                var objID = new ObjID();
                objID.SetIDs(item.ObjType, item.ObjId);

                var objVersion = vault.ObjectOperations.GetLatestObjectVersionAndProperties(objID, true);
                var objVerEx = new ObjVerEx(vault, objVersion);

                if (objVerEx.IsDeleted)
                {
                    SysUtils.ReportInfoToEventLog(
                        $"IHFM: ResidentSiteChange skipped deleted object ObjType={item.ObjType} ObjId={item.ObjId}.");
                    return;
                }

                objVerEx.SaveProperty(configuration.Site, MFDataType.MFDatatypeLookup, item.NewSiteId);

                SysUtils.ReportInfoToEventLog(
                    $"IHFM: ResidentSiteChange updated Site on ObjType={item.ObjType} ObjId={item.ObjId} for ResidentId={item.ResidentObjId}. Queue remaining: {ResidentSiteChangeQueueService.Count()}.");
            }
            catch (Exception ex)
            {
                SysUtils.ReportErrorToEventLog(
                    $"IHFM: ResidentSiteChange failed for ObjType={item.ObjType} ObjId={item.ObjId}.", ex);
            }
        }
    }
}
