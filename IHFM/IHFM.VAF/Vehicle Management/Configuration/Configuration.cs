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
        public MFIdentifier VehicleManagement_ServiceIntervalKm = "MFiles.Property.ServiceIntervalKm";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TotalRunningCosts = "MFiles.Property.TotalRunningCosts";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_LastServiceDate = "MFiles.Property.LastServiceDate";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_LastServiceOdometerReading = "MFiles.Property.LastServiceOdometerReading";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_NextServiceDate = "MFiles.Property.NextServiceDate";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_NextServicekm = "MFiles.Property.NextServicekm";

        [MFPropertyDef]
        public MFIdentifier VehicleManagement_Date = "MFiles.Property.Date";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_CostVehicleManagement = "MFiles.Property.CostVehicle";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_DefectsCost = "MFiles.Property.DefectsCost";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_ActualL100km = "MFiles.Property.ActualL100km";

        [MFPropertyDef]
        public MFIdentifier VehicleManagement_YearOfManafacture = "MFiles.Property.YearOfManafacture";
        
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TyreLf = "MFiles.Property.TyreLf";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TyreLr = "MFiles.Property.TyreLr";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TyreRf = "MFiles.Property.TyreRf";
        [MFPropertyDef]
        public MFIdentifier VehicleManagement_TyreRr = "MFiles.Property.TyreRr";

        [MFPropertyDef]
        public MFIdentifier VehicleManagement_CurrentTyreCondition = "MFiles.Property.CurrentTyreCondition";

        [MFValueListItem(ValueList = "MFiles.Valuelist.CurrentTyreCondition")]
        public MFIdentifier CurrentTyreCondition_Urgent = "{32BC4720-0E02-44F1-BA30-EA7BA97092AB}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.CurrentTyreCondition")]
        public MFIdentifier CurrentTyreCondition_Fair = "{C47FA0EE-AA78-4BA9-9E60-6144EAAA23D4}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.CurrentTyreCondition")]
        public MFIdentifier CurrentTyreCondition_Good = "{A91CEDB8-4CAF-4433-8871-5A676FF228EB}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.CurrentTyreCondition")]
        public MFIdentifier CurrentTyreCondition_New = "{AB36EDE5-30F3-45D8-BD9F-47269BE1C5B6}";

    }
}
