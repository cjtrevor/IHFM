using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionInvoice
    {
        public string RECTYPE { get => "3"; }
        public string CNTBTCH { get => "1"; }
        public string CNTITEM { get; set; }
        public string CNTPAYM { get => ""; }
        public string DATEDUE { get => DateTime.Now.ToString("yyyyMMdd"); }
        public string AMTDUE { get; set; }
        public string DATEDISC { get => ""; }
        public string AMTDISC { get => "0"; }
        public string AMTDUEHC { get; set; }
        public string AMTDISCHC { get => "0"; }

        public string GetHeaders()
        {
            return "RECTYPE,CNTBTCH,CNTITEM,CNTPAYM,DATEDUE,AMTDUE,DATEDISC,AMTDISC,AMTDUEHC,AMTDISCHC";
        }

        public string GetDetails()
        {
            return $"{RECTYPE},{CNTBTCH},{CNTITEM},{CNTPAYM},{DATEDUE},{AMTDUE},{DATEDISC},{AMTDISC},{AMTDUEHC},{AMTDISCHC}";
        }
    }
}
