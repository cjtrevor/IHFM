using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Export.Classes
{
    public class SageTransactionHeader
    {
        public string RECTYPE { get => "1"; }
        public string CNTBTCH { get => "1"; }
        public string CNTITEM { get; set; }
        public string IDCUST { get; set; }
        public string IDINVC { get => "**NEW**"; }
        public string IDSHPT { get => ""; }
        public string SPECINST { get => ""; }
        public string TEXTTRX { get => "1"; }
        public string IDTRX { get => "14"; }
        public string ORDRNBR { get => ""; }
        public string CUSTPO { get => ""; }
        public string INVCDESC { get; set; }
        public string SWPRTINVC { get => "0"; }
        public string INVCAPPLTO { get => ""; }
        public string IDACCTSET { get; set; }
        public string DATEINVC { get => DateTime.Now.ToString("yyyyMMdd"); }
        public string DATEASOF { get => DateTime.Now.ToString("yyyyMMdd"); }
        public string CODECURN { get => "ZAR"; }
        public string RATETYPE { get => "SP"; }
        public string DATEDUE { get => DateTime.Now.ToString("yyyyMMdd"); }

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
