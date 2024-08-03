using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MedsGivenAuto")]
        //public void BeforeCreateNewMDDAutoFinalize(EventHandlerEnvironment env)
        //{
        //    List<int> addedValues = new List<int>();

        //    string pipes = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_MDDValues);

        //    foreach (string val in pipes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        if (addedValues.Contains(Int32.Parse(val)))
        //            continue;

        //        Lookup objLookup = new Lookup() { Item = Int32.Parse(val) };//GetLookupFromVal(env.Vault,Int32.Parse(val));

        //        env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, objLookup.GetAsObjVer());
        //        addedValues.Add(Int32.Parse(val));
        //    }

        //    string timeslot = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_Timeslot);

        //    ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration, env.Vault);
        //    env.ObjVerEx.SetProperty(Configuration.AutoShift, MFDataType.MFDatatypeText, shiftCalculationService.CalculateAutoShiftNumberBySiteIdByResident(env.ObjVerEx,timeslot));
        //}
    }
}
