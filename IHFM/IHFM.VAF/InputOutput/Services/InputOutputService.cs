using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class InputOutputService
    {
        private Vault _vault;
        private Configuration _configuration;

        public InputOutputService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void UpdateInputOutputForShift(ObjVerEx io)
        {
            InputOutputSearchService inputOutputSearchService = new InputOutputSearchService(_vault, _configuration);
            string shiftNumber = io.GetPropertyText(_configuration.Shift);
            int residentLookupId = io.GetLookupID(_configuration.ResidentLookup);

            double intake = 0;
            double outputUrine = 0;
            double outputDiarrhea = 0;
            double outputOther = 0;

            if (io.HasValue(_configuration.TypeOfIntake))
            {
                intake = io.GetProperty(_configuration.VolumeIn).GetValue<double>();
            }

            if(io.HasValue(_configuration.TypeOfOutput))
            {
                if(io.GetLookupID(_configuration.TypeOfOutput) == _configuration.TypeOfOutputUrine.ID)
                {
                    outputUrine = GetNumericOutput(io.GetProperty(_configuration.VolumeOut).GetValueAsLocalizedText());
                }
                else if (io.GetLookupID(_configuration.TypeOfOutput) == _configuration.TypeOfOutputDiarrhea.ID)
                {
                    outputDiarrhea = 1;
                }
                else
                {
                    outputOther = 1;
                }
            }

            //Find object
            ObjVerEx ioTotal = inputOutputSearchService.FindInputOutputTotalByShiftResident(shiftNumber, residentLookupId);

            //Not exist create new one
            if (ioTotal == null)
            { 
                CreateNewInputOutputTotal(shiftNumber, residentLookupId, intake,outputUrine,outputDiarrhea,outputOther);
                return;
            }

            //Update values
            UpdateInputOutputTotal(ioTotal, intake, outputUrine, outputDiarrhea, outputOther);
        }

        private int GetNumericOutput(string volumeOut)
        {
            int volume;

            if (!Int32.TryParse(volumeOut.Substring(volumeOut.IndexOf('(') + 1, 1), out volume))
                volume = 1;

            return volume;
        }
        public void CreateNewInputOutputTotal(string shiftNumber, int residentLookupId, double intake, double outputUrine, double outputDiarrhea, double outputOther)
        {
            int ioObjectID = _vault.ObjectTypeOperations.GetObjectTypeIDByAlias(_configuration.IntakeOutputTotal.Alias);

            MFPropertyValuesBuilder mfProperties = new MFPropertyValuesBuilder(_vault)
                .SetClass(_configuration.IntakeOutputTotalClass)
                .Add(_configuration.ShiftIO, MFDataType.MFDatatypeText, shiftNumber)
                .Add(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentLookupId)
                .Add(_configuration.IntakeTotal, MFDataType.MFDatatypeFloating, intake)
                .Add(_configuration.OutputUrine, MFDataType.MFDatatypeFloating, outputUrine)
                .Add(_configuration.OutputDiarrhea, MFDataType.MFDatatypeFloating, outputDiarrhea)
                .Add(_configuration.OutputOther, MFDataType.MFDatatypeFloating, outputOther);

            _vault.ObjectOperations.CreateNewObjectExQuick(ioObjectID, mfProperties.Values);
        }
        public void UpdateInputOutputTotal(ObjVerEx ioTotal, double intake, double outputUrine, double outputDiarrhea, double outputOther)
        {
            double currentIntake = ioTotal.GetProperty(_configuration.IntakeTotal).GetValue<double>();
            double currentOutputUrine = ioTotal.GetProperty(_configuration.OutputUrine).GetValue<double>();
            double currentOutputDiarrhea = ioTotal.GetProperty(_configuration.OutputDiarrhea).GetValue<double>();
            double currentOutputOther = ioTotal.GetProperty(_configuration.OutputOther).GetValue<double>();

            bool start = ioTotal.StartRequireCheckedOut();
            ioTotal.SetProperty(_configuration.IntakeTotal, MFDataType.MFDatatypeFloating, currentIntake + intake);
            ioTotal.SetProperty(_configuration.OutputUrine, MFDataType.MFDatatypeFloating, currentOutputUrine + outputUrine);
            ioTotal.SetProperty(_configuration.OutputDiarrhea, MFDataType.MFDatatypeFloating, currentOutputDiarrhea + outputDiarrhea);
            ioTotal.SetProperty(_configuration.OutputOther, MFDataType.MFDatatypeFloating, currentOutputOther + outputOther);
            ioTotal.SaveProperties();
            ioTotal.EndRequireCheckedOut(start);
        }
    }
}
