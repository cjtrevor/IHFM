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
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.MedsGivenAuto")]
        //public void AfterCreateNewMDDAutoFinaliza(EventHandlerEnvironment env)
        //{
        //    string pipes = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_MDDValues);

        //    foreach(string val in pipes.Split(new string[] { "|"},StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, Int32.Parse(val));
        //    }

        //    //env.ObjVerEx.SaveProperties();
        //}
    }
}
