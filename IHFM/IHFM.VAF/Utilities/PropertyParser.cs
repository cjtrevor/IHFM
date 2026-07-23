using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public static class PropertyParser
    {
        public static string ExtractValueFromString(string input, string startChar = "(", string endChar = ")", int splitIndex = 1)
        {
            return input.Split(new string[] { startChar, endChar }, StringSplitOptions.RemoveEmptyEntries)[splitIndex];
        }

        public static int ExtractPropertyValue(ObjVerEx obj, MFIdentifier property)
        {
            string objValue = obj.GetProperty(property).GetValueAsLocalizedText();
            int convObjValue;
            if (!Int32.TryParse(ExtractValueFromString(objValue), out convObjValue))
            {
                throw new Exception($"{objValue} could not be converted to an integer value. Propert IDy:{property.ID}");
            }

            return convObjValue;
        }

        public static int ExtractPropertyValueSquareBraces(ObjVerEx obj, MFIdentifier property)
        {
            string objValue = obj.GetProperty(property).GetValueAsLocalizedText();
            int convObjValue;
            if (!Int32.TryParse(ExtractValueFromString(objValue, "[", "]", 0), out convObjValue))
            {
                throw new Exception($"{objValue} could not be converted to an integer value. Propert IDy:{property.ID}");
            }

            return convObjValue;
        }
    }
}
