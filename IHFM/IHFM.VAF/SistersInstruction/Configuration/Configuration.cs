using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Property Definition
        [MFPropertyDef]
        public MFIdentifier SistersInstructionActivity_SistersIntructionDropdown = "MFiles.Property.PreventativeCareInstruction";
        [MFPropertyDef]
        public MFIdentifier SistersInstruction_DateLastActioned = "MFiles.Property.DateLastActioned";

        [MFPropertyDef]
        public MFIdentifier SistersInstruction_TimeLastActioned = "MFiles.Property.TimeLastActioned";


    }
}
