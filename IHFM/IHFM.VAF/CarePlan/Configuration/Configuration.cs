﻿using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFObjType(Required = true)]
        public MFIdentifier CarePlanObject = "MFiles.Object.CarePlan";
    }
}
