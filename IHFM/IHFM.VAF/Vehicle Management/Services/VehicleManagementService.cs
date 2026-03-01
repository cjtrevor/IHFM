using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using static MFiles.VAF.Configuration.ValidationResultForValidation;

namespace IHFM.VAF
{
    public class VehicleManagementService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public VehicleManagementService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void UpdateVehicleTotalRunningCosts(Lookup vehicleLookup, double amount)
        {
            ObjVerEx vehicle = new ObjVerEx(_vault, vehicleLookup);

            var currentTotalRunningCosts = vehicle.GetPropertyAsDouble(_configuration.VehicleManagement_TotalRunningCosts);
            vehicle.SetProperty(_configuration.VehicleManagement_TotalRunningCosts, MFDataType.MFDatatypeFloating, currentTotalRunningCosts + amount);
            vehicle.SaveProperties();
        }

        public ObjVerEx GetLatestFuelSlip(int vehicleId, int existingObjectId = 0)
        {
            ObjVerEx result = null;

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(_configuration.VehicleManagement_FuelSlipClass);
            search.Property(_configuration.VehicleManagement_Vehicle, MFDataType.MFDatatypeLookup, vehicleId);
            search.PropertyNotEmpty(_configuration.VehicleManagement_Date);

            var searchResults = search.FindEx();
            var currentMostRecentDate = DateTime.MinValue;

            foreach (ObjVerEx item in searchResults.Where(x => x.ID != existingObjectId).ToList())
            {
                var date = item.GetPropertyAsDateTime(_configuration.VehicleManagement_Date);
                if (date.HasValue && date.Value > currentMostRecentDate)
                {
                    currentMostRecentDate = date.Value;
                    result = item;
                }
            }

            return result;
        }

        public List<ObjVerEx> GetFinesByVehicle(int vehicleId)
        {
            var results = new List<ObjVerEx>();

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(_configuration.VehicleManagement_FinesClass);
            search.Property(_configuration.VehicleManagement_Vehicle, MFDataType.MFDatatypeLookup, vehicleId);
            //search.Property(_configuration.SOMETHING_PAIN???, MFDataType.MFDatatypeBoolean, true/false);
            search.PropertyNotEmpty(_configuration.VehicleManagement_AmountR);

            results = search.FindEx();

            return results;
        }

        public List<ObjVerEx> GetVehicleInspectionsByVehicle(int vehicleId)
        {
            var results = new List<ObjVerEx>();

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(_configuration.VehicleManagement_VehicleInspectionClass);
            search.Property(_configuration.VehicleManagement_Vehicle, MFDataType.MFDatatypeLookup, vehicleId);
            search.PropertyNotEmpty(_configuration.VehicleManagement_DefectsCost);

            results = search.FindEx();

            return results;
        }

        public List<ObjVerEx> GetVehicleMaintenanceByVehicle(int vehicleId)
        {
            var results = new List<ObjVerEx>();

            MFSearchBuilder search = new MFSearchBuilder(_vault);
            search.Class(_configuration.VehicleManagement_VehicleMaintenanceClass);
            search.Property(_configuration.VehicleManagement_Vehicle, MFDataType.MFDatatypeLookup, vehicleId);
            search.PropertyNotEmpty(_configuration.VehicleManagement_CostVehicleManagement);

            results = search.FindEx();

            return results;
        }

    }
}
