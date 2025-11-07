using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IHFM.VAF
{
    public class ShiftAllocationSearchService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public ShiftAllocationSearchService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public List<ObjVerEx> SearchForExistingStaffShiftAllocations(int staffMemberLookupId, DateTime searchDate, int existingObjectId = 0)
        {
            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(_configuration.ShiftAllocation_Class);
            search.Date(_configuration.ShiftAllocation_StartDateTime, searchDate);
            search.References(_configuration.ShiftAllocation_StaffAttending, staffMemberLookupId);

            var results = search.FindEx();

            return results.Where(ov => ov.ID != existingObjectId).ToList();
        }
    }
}
