using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class QMRSiteDetail
    {
		public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteCareManager { get; set; }
        public int QuarterNumber { get; set; }
        public int YearNumber { get; set; }
        public int HealthStatusIndependant { get; set; }
        public int HealthStatusAssisted { get; set; }
        public int HealthStatusDependant { get; set; }
        public int MentallyFrail { get; set; }
        public int PhysicallyFrail { get; set; }
        public int PartiallyFrail { get; set; }
        public int TotallyFrail { get; set; }
        public int Diabetics { get; set; }
        public int InfectiousDisease { get; set; }
        public int PressureSores { get; set; }
        public int WheelChairCases { get; set; }
    }
}
