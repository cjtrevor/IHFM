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
            items.AddRange(FindRelatedItems(residentObjId, newSiteId, _configuration.CarePlanObject));
            items.AddRange(FindRelatedItems(residentObjId, newSiteId, _configuration.VitalsRecordObject));

            if (items.Count > 0)
            {
                ResidentSiteChangeQueueService.Enqueue(items);
                SysUtils.ReportInfoToEventLog(
                    $"IHFM: ResidentSiteChange enqueued {items.Count} object(s) for ResidentId={residentObjId} NewSiteId={newSiteId}.");
            }
        }

        private List<ResidentSiteChangeQueueItem> FindRelatedItems(int residentObjId, int newSiteId, MFiles.VAF.Configuration.MFIdentifier objTypeIdentifier)
        {
            var results = new List<ResidentSiteChangeQueueItem>();

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.ObjType(objTypeIdentifier);
            search.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentObjId);
            search.Deleted(false);

            var found = search.FindEx();
            foreach (var obj in found)
            {
                //PRAN NOTES
                //Class will be added in future
                results.Add(new ResidentSiteChangeQueueItem
                {
                    ObjId = obj.ObjVer.ID,
                    ObjType = obj.ObjVer.Type,
                    NewSiteId = newSiteId,
                    ResidentObjId = residentObjId
                });
            }

            return results;
        }
    }
}
