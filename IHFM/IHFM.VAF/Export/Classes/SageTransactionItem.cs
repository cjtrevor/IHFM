using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionItem
    {
        public string RECTYPE { get => ""; }
        public string CNTBTCH { get => ""; }
        public string CNTITEM { get => ""; }
        public string CNTLINE { get; set; }
        public string IDITEM { get => ""; }
        public string IDDIST { get; set; }
        public string TEXTDESC { get; set; }
        public string UNITMEAS { get => "0"; }
        public string QTYINVC { get => ""; }
        public string AMTCOST { get => "0"; }
        public string AMTPRIC { get => "0"; }
        public string AMTEXTN { get; set; }
        public string AMTCOGS { get => "0"; }
        public string AMTTXBL { get; set; }
        public string TOTTAX { get; set; }
        public string BASETAX1 { get => ""; }
        public string TAXSTTS1 { get => ""; }
        public string SWTAXINCL1 { get => ""; }
        public string RATETAX1 { get => ""; }
        public string AMTTAX1 { get => ""; }
    }
}
