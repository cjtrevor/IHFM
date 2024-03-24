using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class LaundryCalculationService
    {
        public LaundryCalculationService()
        {

        }

        public int GetScoreSumFromListValues(ObjVerEx item, List<MFIdentifier> configs)
        {
            int score = 0;

            configs.ForEach(x => {
                if (item.HasValue(x))
                {
                    score += Int32.Parse(item.GetProperty(x).GetValueAsLocalizedText());
                }
            });

            return score;
        }
    }
}
