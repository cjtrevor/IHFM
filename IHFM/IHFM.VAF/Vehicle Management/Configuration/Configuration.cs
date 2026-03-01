using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFObject]
        public MFIdentifier VehicleManagement_Object = "MFiles.Object.VehicleManagement";

        [MFClass]
        public MFIdentifier VehicleManagement_FinesClass = "MFiles.Class.Fines";
        [MFClass]
        public MFIdentifier VehicleManagement_FuelSlipClass = "MFiles.Class.FuelSlip";
        [MFClass]
        public MFIdentifier VehicleManagement_VehicleClass = "MFiles.Class.Vehicle";
        [MFClass]
        public MFIdentifier VehicleManagement_VehicleInspectionClass = "MFiles.Class.VehicleInspection";
        [MFClass]
        public MFIdentifier VehicleManagement_VehicleMaintenanceClass = "MFiles.Class.VehicleMaintenance";

        [MFPropertyDef]
        public MFIdentifier VehicleManagement_Vehicle = "MFiles.Property.Vehicle";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_OdometerReading = "MFiles.Property.OdometerReading";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_AmountR = "MFiles.Property.AmountR";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_Litres = "MFiles.Property.Litres";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_ServiceIntervalMonths = "MFiles.Property.ServiceIntervalmonths";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TotalRunningCosts = "MFiles.Property.TotalRunningCosts";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_LastServiceDate = "MFiles.Property.LastServiceDate";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_LastServiceOdometerReading = "MFiles.Property.LastServiceOdometerReading";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_NextServiceDate = "MFiles.Property.NextServiceDate";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_Date = "MFiles.Property.Date";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_CostVehicleManagement = "MFiles.Property.CostVehicle";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_DefectsCost = "MFiles.Property.DefectsCost";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_ActualL100km = "MFiles.Property.ActualL100km";
    }
}
