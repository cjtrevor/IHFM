using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class QMRIncidentExport
    {
        public int ObjId { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteCareManager { get; set; }
        public int QuarterNumber { get; set; }
        public int YearNumber { get; set; }
        public string ResidentName { get; set; }
        public string DateOfIncident { get; set; }
        public string TimeOfIncident { get; set; }
        public string Benzo { get; set; }
        public string Cause { get; set; }
        public string Injury { get; set; }
        public string Treatment { get; set; }
    }
}
