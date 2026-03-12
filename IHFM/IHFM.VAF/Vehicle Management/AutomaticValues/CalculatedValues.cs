using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
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
        [PropertyCustomValue("MFiles.Property.AgeAutomated")]
        public TypedValue SetVehicleAge(PropertyEnvironment env)
        {
            var vehicleAge = 0;

            if(env.ObjVerEx.HasProperty(Configuration.VehicleManagement_YearOfManafacture) && env.ObjVerEx.HasValue(Configuration.VehicleManagement_YearOfManafacture))
            {
                var yearOfManufacture = env.ObjVerEx.GetPropertyAsDateTime(Configuration.VehicleManagement_YearOfManafacture)??DateTime.Now;

                vehicleAge = DateTime.Now.Year - yearOfManufacture.Year;
            }

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, vehicleAge);

            return calculated;
        }
    }
}
