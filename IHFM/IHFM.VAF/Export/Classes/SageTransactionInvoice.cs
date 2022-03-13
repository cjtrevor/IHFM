using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionInvoice
    {
        public string RECTYPE { get => ""; }
        public string CNTBTCH { get => ""; }
        public string CNTITEM { get => ""; }
        public string CNTPAYM { get => ""; }
        public string DATEDUE { get; set; }
        public string AMTDUE { get; set; }
        public string DATEDISC { get => ""; }
        public string AMTDISC { get => ""; }
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
