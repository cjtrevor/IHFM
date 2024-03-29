﻿using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class DailyCareSearchService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public DailyCareSearchService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public ObjVerEx GetDailyCareByResidentAndShift(int residentId, string shift)
        {
            MFSearchBuilder search = new MFSearchBuilder(vault);
            search.Class(configuration.DailyCareClass);
            search.Property(configuration.Shift, MFDataType.MFDatatypeText, shift);
            search.Property(configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            if (search.FindEx().Count > 1)
                return search.FindOneEx();

            return null;         
        }
    }
}
