using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;
namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.Resident")]
        public void SetRoomVacantWhenInactive(EventHandlerEnvironment env)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            bool active = env.ObjVerEx.GetProperty(Configuration.Active).GetValue<bool>();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(env.Vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(!active, currentRoomObjVerEx);
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.Resident")]
        public void SetDiscountValueIfPercentage(EventHandlerEnvironment env)
        {
            if(env.ObjVerEx.HasValue(Configuration.DiscountPercentage) && env.ObjVerEx.HasValue(Configuration.RoomTariff))
            {
                double tariff = double.Parse(env.ObjVerEx.GetProperty(Configuration.RoomTariff).GetValueAsLocalizedText());
                double discountPerc = env.ObjVerEx.GetProperty(Configuration.DiscountPercentage).GetValue<double>();

                double discountValue = tariff * discountPerc / 100;

                env.ObjVerEx.SaveProperty(Configuration.DiscountRandValue, MFDataType.MFDatatypeFloating, discountValue);
            }
        }
    }
}
