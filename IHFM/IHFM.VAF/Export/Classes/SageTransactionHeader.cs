using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionHeader
    {
        public string RECTYPE { get => ""; }
        public string CNTBTCH { get => "1"; }
        public string CNTITEM { get => "1"; }
        public string IDCUST { get; set; }
        public string IDINVC { get => ""; }
        public string IDSHPT { get => "1"; }
        public string SPECINST { get => ""; }
        public string TEXTTRX { get => "1"; }
        public string IDTRX { get => "4"; }
        public string ORDRNBR { get => ""; }
        public string CUSTPO { get => ""; }
        public string INVCDESC { get; set; }
        public string SWPRTINVC { get => "0"; }
        public string INVCAPPLTO { get => ""; }
        public string IDACCTSET { get; set; }
        public string DATEINVC { get => ""; }
        public string DATEASOF { get => ""; }
        public string CODECURN { get => ""; }
        public string RATETYPE { get => ""; }
        public string DATEDUE { get => ""; }

        public string GetHeaders()
        {
            return "RECTYPE,CNTBTCH,CNTITEM,IDCUST,IDINVC,IDSHPT,SPECINST,TEXTTRX,IDTRX,ORDRNBR,CUSTPO,INVCDESC,SWPRTINVC,INVCAPPLTO,IDACCTSET,DATEINVC,DATEASOF,CODECURN,RATETYPE,DATEDUE";
        }

        public string GetDetails()
        {
            return $"{RECTYPE},{CNTBTCH},{CNTITEM},{IDCUST},{IDINVC},{IDSHPT},{SPECINST},{TEXTTRX},{IDTRX},{ORDRNBR},{CUSTPO},{INVCDESC},{SWPRTINVC},{INVCAPPLTO},{IDACCTSET},{DATEINVC},{DATEASOF},{CODECURN},{RATETYPE},{DATEDUE}";
        }
    }
}
