using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class QMRDeathsExport
    {
        public int ObjId { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public int QuarterNumber { get; set; }
        public int YearNumber { get; set; }
        public string ResidentName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string DateOfDeath { get; set; }
        public string MedicalConditions { get; set; }
    }
}
