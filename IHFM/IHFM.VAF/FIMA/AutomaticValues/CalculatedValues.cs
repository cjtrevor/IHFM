using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System.Collections.Generic;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.FimMotorSubtotalScore", Priority = 100)]
        public TypedValue SetTotalFIMAMotorValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            FIMACalculationService service = new FIMACalculationService();

            List<MFIdentifier> configs = new List<MFIdentifier>
            {
                Configuration.FIMA_Eating
                ,Configuration.FIMA_Grooming
                ,Configuration.FIMA_Bathing
                ,Configuration.FIMA_DressingUpper
                ,Configuration.FIMA_DressingLower
                ,Configuration.FIMA_Toileting
                ,Configuration.FIMA_BladderManagement
                ,Configuration.FIMA_BowelManagement
                ,Configuration.FIMA_TransferScore
                ,Configuration.FIMA_Toilet
                ,Configuration.FIMA_Tub
                ,Configuration.FIMA_Locomotion
                ,Configuration.FIMA_Stairs
            };

            calculated.SetValue(MFDataType.MFDatatypeInteger, service.GetScoreSumFromListValues(env.ObjVerEx,configs));

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.FimCognitiveSubtotalScore", Priority = 100)]
        public TypedValue SetTotalFIMACognitiveValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            FIMACalculationService service = new FIMACalculationService();

            List<MFIdentifier> configs = new List<MFIdentifier>
            {
                Configuration.FIMA_Comprehension
                ,Configuration.FIMA_Expression
                ,Configuration.FIMA_SocialInteraction
                ,Configuration.FIMA_ProblemSolving
                ,Configuration.FIMA_Memory
            };

            calculated.SetValue(MFDataType.MFDatatypeInteger, service.GetScoreSumFromListValues(env.ObjVerEx, configs));

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.FimOverallScore", Priority = 100)]
        public TypedValue SetTotalFIMAOverallValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int totalMotor = env.ObjVerEx.GetProperty(Configuration.FIMA_TotalMotorValue).GetValue<int>();
            int totalCognitive = env.ObjVerEx.GetProperty(Configuration.FIMA_TotalCognitiveValue).GetValue<int>();

            calculated.SetValue(MFDataType.MFDatatypeInteger, totalMotor + totalCognitive);

            return calculated;
        }
    }
}
