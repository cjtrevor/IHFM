using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionItem
    {
        public string RECTYPE { get => "2"; }
        public string CNTBTCH { get => "1"; }
        public string CNTITEM { get; set; }
        public string CNTLINE { get; set; }
        public string IDITEM { get => ""; }
        public string IDACCTREV { get; set; }
        public string TEXTDESC { get; set; }
        public string UNITMEAS { get => ""; }
        public string QTYINVC { get => "0"; }
        public string AMTCOST { get => "0"; }
        public string AMTPRIC { get => "0"; }
        public string AMTEXTN { get; set; }
        public string AMTCOGS { get => "0"; }
        public string AMTTXBL { get; set; }
        public string TOTTAX { get; set; }
        public string BASETAX1 { get; set; }
        public string TAXSTTS1 { get => "1"; }
        public string SWTAXINCL1 { get => "1"; }
        public string RATETAX1 { get => ""; }

        public string GetHeaders()
        {
            return "RECTYPE,CNTBTCH,CNTITEM,CNTLINE,IDITEM,IDACCTREV,TEXTDESC,UNITMEAS,QTYINVC,AMTCOST,AMTPRIC,AMTEXTN,AMTCOGS,AMTTXBL,TOTTAX,BASETAX1,TAXSTTS1,SWTAXINCL1,RATETAX1";
        }

        public string GetDetails()
        {
            return $"{RECTYPE}, {CNTBTCH},{CNTITEM},{CNTLINE},{IDITEM},{IDACCTREV},{TEXTDESC},{UNITMEAS},{QTYINVC},{AMTCOST},{AMTPRIC},{AMTEXTN},{AMTCOGS},{AMTTXBL},{TOTTAX},{BASETAX1},{TAXSTTS1},{SWTAXINCL1},{RATETAX1}";
        }
    }
}
