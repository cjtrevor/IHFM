using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentReport_InvestigationDone = "MFiles.Property.InvestigationDone";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentReport_RegardingIncident = "MFiles.Property.IncidentReported";
    }
}
