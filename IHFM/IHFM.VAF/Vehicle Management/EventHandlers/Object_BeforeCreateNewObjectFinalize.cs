using IHFM.VAF.Email.Services;
using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {

        //UPDATE - BEFORE FINALIZE - OBJECT
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = "MFiles.Object.VehicleManagement")]
        public void VehicleManagement_UpdateProperties(EventHandlerEnvironment env)
        {
            if (!env.ObjVerEx.HasProperty(Configuration.VehicleManagement_Vehicle) || !env.ObjVerEx.HasProperty(Configuration.VehicleManagement_OdometerReading))
                return;

            var vehicleLookup = env.ObjVerEx.GetProperty(Configuration.VehicleManagement_Vehicle).TypedValue.GetValueAsLookup();
            var vehicle = new ObjVerEx(env.Vault, vehicleLookup);

            var currentOdometerReading = vehicle.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;
            var newOdometerReading = 0d;

            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);
            foreach (PropertyValueChange change in changes.Changed)
            {
                if (change.PropertyDef == Configuration.VehicleManagement_OdometerReading.ID)
                {
                    switch (change.ChangeType)
                    {
                        case PropertyValueChangeType.None:
                            break;
                        case PropertyValueChangeType.Added:
                            newOdometerReading = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;
                            if (newOdometerReading > currentOdometerReading)
                            {
                                vehicle.SaveProperty(Configuration.VehicleManagement_OdometerReading, MFDataType.MFDatatypeFloating, newOdometerReading);
                            }
                            break;
                        case PropertyValueChangeType.Modified:
                            //totalAmount = change.NewValue.GetValue<double>() - change.OldValue.GetValue<double>();
                            break;
                        case PropertyValueChangeType.Removed:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //UPDATE - BEFORE FINALIZE
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.Fines")]
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.VehicleInspection")]
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.VehicleMaintenance")]
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.FuelSlip")]
        public void VehicleManagement_Fines_BeforeCheckInChanges(EventHandlerEnvironment env)
        {
            MFIdentifier propertyIdentifier;

            var vehicleLookup = env.ObjVerEx.GetProperty(Configuration.VehicleManagement_Vehicle).TypedValue.GetValueAsLookup();
            ObjVerEx vehicle = new ObjVerEx(env.Vault, vehicleLookup);

            var classId = env.GetObjectClass();
            if (classId == Configuration.VehicleManagement_VehicleInspectionClass.ID)
            {
                propertyIdentifier = Configuration.VehicleManagement_DefectsCost;

                var tyreLf = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_TyreLf) ?? 0;
                var tyreLr = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_TyreLr) ?? 0;
                var tyreRf = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_TyreRf) ?? 0;
                var tyreRr = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_TyreRr) ?? 0;

                var lowestTyreThread = Math.Min(
                    Math.Min(tyreLf, tyreLr),
                    Math.Min(tyreRf, tyreRr)
                    );

                var currentTyreCondition = Configuration.CurrentTyreCondition_New.ID;

                if (lowestTyreThread <= 2)
                {
                    currentTyreCondition = Configuration.CurrentTyreCondition_Urgent.ID;
                }
                else if (lowestTyreThread > 2 && lowestTyreThread <= 4)
                {
                    currentTyreCondition = Configuration.CurrentTyreCondition_Fair.ID;
                }
                else if(lowestTyreThread > 4 && lowestTyreThread <= 6)
                {
                    currentTyreCondition = Configuration.CurrentTyreCondition_Good.ID;
                }

                vehicle.SaveProperty(Configuration.VehicleManagement_CurrentTyreCondition, MFDataType.MFDatatypeLookup, currentTyreCondition);
            }
            else if (classId == Configuration.VehicleManagement_VehicleMaintenanceClass.ID)
            {
                propertyIdentifier = Configuration.VehicleManagement_CostVehicleManagement;
            }
            else
            {
                propertyIdentifier = Configuration.VehicleManagement_AmountR;
            }

            UpdateVehicleTotalRunningCostsByProperty(env, propertyIdentifier, vehicleLookup);
        }

        //CREATE - BEFORE FINALIZE - CLASS_FUEL_SLIP
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.FuelSlip")]
        public void VehicleManagement_FuelSlip_BeforeCreateNewObjectFinalize(EventHandlerEnvironment env)
        {
            var vehicleManagementService = new VehicleManagementService(env.Vault, Configuration);

            var vehicleLookup = env.ObjVerEx.GetProperty(Configuration.VehicleManagement_Vehicle).TypedValue.GetValueAsLookup();
            var latestFuelSlip = vehicleManagementService.GetLatestFuelSlip(vehicleLookup.Item, env.ObjVer.ID);

            if (latestFuelSlip == null)
                return;

            var litres = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_Litres) ?? 0;

            var currentOdometerReading = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;
            var lastOdometerReading = latestFuelSlip.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;

            var distanceTravelled = currentOdometerReading - lastOdometerReading;

            if (distanceTravelled <= 0 || litres <= 0)
                return;

            var litresPer100Km = (litres / distanceTravelled) * 100;

            ObjVerEx vehicle = new ObjVerEx(env.Vault, vehicleLookup);
            vehicle.SetProperty(Configuration.VehicleManagement_ActualL100km, MFDataType.MFDatatypeFloating, litresPer100Km);
            vehicle.SaveProperties();
        }

        //CREATE - BEFORE FINALIZE - CLASS_VEHICLE_MAINTENANCE
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.VehicleMaintenance")]
        public void VehicleManagement_VehicleMaintenance_BeforeCreateNewObjectFinalize(EventHandlerEnvironment env)
        {
            var currentServiceDate = env.ObjVerEx.GetPropertyAsDateTime(Configuration.VehicleManagement_Date) ?? DateTime.Now;
            if (currentServiceDate > DateTime.Today)
                throw new InvalidOperationException("Service date cannot be in the future.");

            var vehicleLookup = env.ObjVerEx.GetProperty(Configuration.VehicleManagement_Vehicle).TypedValue.GetValueAsLookup();
            ObjVerEx vehicle = new ObjVerEx(env.Vault, vehicleLookup);

            var vehicleLastServiceDate = vehicle.GetPropertyAsDateTime(Configuration.VehicleManagement_LastServiceDate);
            if (vehicleLastServiceDate.HasValue && currentServiceDate < vehicleLastServiceDate.Value)
                return;

            var serviceIntervalMonths = vehicle.GetPropertyAsInteger(Configuration.VehicleManagement_ServiceIntervalMonths) ?? 0;
            var vehicleOdometerReading = vehicle.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;
            var serviceIntervalKmsText = vehicle.GetPropertyText(Configuration.VehicleManagement_ServiceIntervalKm);
            Int32.TryParse(serviceIntervalKmsText, out int serviceIntervalKms);

            var odometerReading = env.ObjVerEx.GetPropertyAsDouble(Configuration.VehicleManagement_OdometerReading) ?? 0;

            vehicle.SetProperty(Configuration.VehicleManagement_LastServiceDate, MFDataType.MFDatatypeDate, currentServiceDate);
            vehicle.SetProperty(Configuration.VehicleManagement_NextServiceDate, MFDataType.MFDatatypeDate, currentServiceDate.AddMonths(serviceIntervalMonths));
            vehicle.SetProperty(Configuration.VehicleManagement_LastServiceOdometerReading, MFDataType.MFDatatypeFloating, vehicleOdometerReading);
            vehicle.SetProperty(Configuration.VehicleManagement_NextServicekm, MFDataType.MFDatatypeFloating, odometerReading + serviceIntervalKms);

            vehicle.SaveProperties();
        }

        private void UpdateVehicleTotalRunningCostsByProperty(EventHandlerEnvironment env, MFIdentifier costProperty, Lookup vehicleLookup)
        {
            var vehicleManagementService = new VehicleManagementService(env.Vault, Configuration);

            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);
            foreach (PropertyValueChange change in changes.Changed)
            {
                if (change.PropertyDef == costProperty.ID)
                {
                    var totalAmount = 0d;

                    switch (change.ChangeType)
                    {
                        case PropertyValueChangeType.None:
                            break;
                        case PropertyValueChangeType.Added:
                            totalAmount = env.ObjVerEx.GetPropertyAsDouble(costProperty) ?? 0;
                            break;
                        case PropertyValueChangeType.Modified:
                            totalAmount = change.NewValue.GetValue<double>() - change.OldValue.GetValue<double>();
                            break;
                        case PropertyValueChangeType.Removed:
                            break;
                        default:
                            break;
                    }

                    vehicleManagementService.UpdateVehicleTotalRunningCosts(vehicleLookup, totalAmount);
                }
            }
        }
    }
}
