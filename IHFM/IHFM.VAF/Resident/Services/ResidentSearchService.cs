﻿using MFiles.VAF.Common;
using MFilesAPI;
using System.Collections.Generic;

namespace IHFM.VAF
{
    public class ResidentSearchService
    {
        public List<ObjVerEx> GetAllResidents(Vault vault, Configuration configuration)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(vault);
            mFSearchBuilder.ObjType(configuration.ResidentObject);
            mFSearchBuilder.Deleted(false);
            return mFSearchBuilder.FindEx();
        }
    }
}