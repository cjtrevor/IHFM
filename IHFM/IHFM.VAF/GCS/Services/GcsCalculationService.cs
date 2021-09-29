using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class GcsCalculationService
    {
        private Configuration _configuration;
        public GcsCalculationService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public string GetGcsScore(ObjVerEx objVerEx)
        {
            bool GcsRequired = objVerEx.GetProperty(_configuration.GCSRequired).GetValue<bool>();

            if (!GcsRequired)
                return "";

            int motorResponse = GetScoreValue(objVerEx.GetProperty(_configuration.BestMotorResponse).GetValueAsLocalizedText());
            int verbalResponse = GetScoreValue(objVerEx.GetProperty(_configuration.BestVerbalResponse).GetValueAsLocalizedText());
            int eyeOpening = GetScoreValue(objVerEx.GetProperty(_configuration.EyeOpeningResponse).GetValueAsLocalizedText());

            return (motorResponse + verbalResponse + eyeOpening).ToString();
        }

        private int GetScoreValue(string score)
        {
            return Int32.Parse(score.Substring(0, 1));
        }
    }
}
