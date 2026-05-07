using IHFM.VAF.Resident.Models;
using IHFM.VAF.Resident.Services;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System.Collections.Generic;

namespace IHFM.VAF.Resident.Services
{
    /// <summary>
    /// Finds all objects related to a Resident that need their Site property updated,
    /// and enqueues them for sequential background processing.
    /// </summary>
    public class ResidentSiteChangeEnqueueService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public ResidentSiteChangeEnqueueService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        /// <summary>
        /// Enqueues CarePlan and VitalsRecord objects linked to the given resident
        /// so their Site property gets updated to <paramref name="newSiteId"/>.
        /// </summary>
        public void EnqueueRelatedObjectsForResident(int residentObjId, int newSiteId)
        {
            var items = new List<ResidentSiteChangeQueueItem>();

            //PRAN NOTES
            //will Want to chang this to something more dynamic like a MFilesAdmin config or something?
            items.AddRange(FindRelatedItems(residentObjId, newSiteId, _configuration.CarePlanClass));
            items.AddRange(FindRelatedItems(residentObjId, newSiteId, _configuration.VitalsRecordClass));

            if (items.Count > 0)
            {
                ResidentSiteChangeQueueService.Enqueue(items);
                SysUtils.ReportInfoToEventLog(
                    $"IHFM: ResidentSiteChange enqueued {items.Count} object(s) for ResidentId={residentObjId} NewSiteId={newSiteId}.");
            }
        }

        private List<ResidentSiteChangeQueueItem> FindRelatedItems(int residentObjId, int newSiteId, MFiles.VAF.Configuration.MFIdentifier classIdentifier)
        {
            var results = new List<ResidentSiteChangeQueueItem>();

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(classIdentifier);
            search.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentObjId);
            search.PropertyNotMissing(_configuration.SiteList);
            search.Deleted(false);

            var found = search.FindEx();
            foreach (var obj in found)
            {
                results.Add(new ResidentSiteChangeQueueItem
                {
                    ObjId = obj.ObjVer.ID,
                    ObjType = obj.ObjVer.Type,
                    //ClassType = obj.Class,
                    NewSiteId = newSiteId,
                    //ResidentObjId = residentObjId
                });
            }

            return results;
        }
    }
}
