using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;
namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.Resident")]
        public void BeforeCheckInRoomChanges(EventHandlerEnvironment env)
        {
            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            foreach (PropertyValueChange change in changes.Changed)
            {
                if(change.PropertyDef == Configuration.Active.ID && change.ChangeType == PropertyValueChangeType.Modified)
                {
                    SetRoomVacantWhenInactive(env);
                }

                if (change.PropertyDef == Configuration.RoomTariff.ID && change.ChangeType == PropertyValueChangeType.Modified)
                {
                    SetDiscountValueIfPercentage(env);
                }
                
                if(DevelopmentUtility.IsDevMode(env.ObjVerEx, Configuration)) //TODO: Remove Dev Check
                { 
                    if (change.PropertyDef == Configuration.CurrentRoom.ID && change.ChangeType == PropertyValueChangeType.Modified)
                    {
                        UpdateRoomTariffOnRoomChange(env);
                        SetDiscountValueIfPercentage(env);
                    }
                }
            }
        }

        public void UpdateRoomTariffOnRoomChange(EventHandlerEnvironment env)
        {
            Lookup roomLookup = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            ObjVerEx room = new ObjVerEx(env.Vault, roomLookup);
            Lookup selectedTariff = room.GetProperty(Configuration.RoomTariff).TypedValue.GetValueAsLookup();

            if(selectedTariff != null)
                env.ObjVerEx.SaveProperty(Configuration.RoomTariff, MFDataType.MFDatatypeLookup, selectedTariff.Item);
        }

        public void SetRoomVacantWhenInactive(EventHandlerEnvironment env)
        {
            RoomPropertyService roomPropertyService = new RoomPropertyService(Configuration);
            Lookup currentRoom = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            bool active = env.ObjVerEx.GetProperty(Configuration.Active).GetValue<bool>();

            ObjVerEx currentRoomObjVerEx = new ObjVerEx(env.Vault, currentRoom);
            roomPropertyService.SetRoomVacantStatus(!active, currentRoomObjVerEx);
        }

        public void SetDiscountValueIfPercentage(EventHandlerEnvironment env)
        {
            double tariff;
            if(!double.TryParse(env.ObjVerEx.GetProperty(Configuration.RoomTariff).GetValueAsLocalizedText(),out tariff))
            {
                throw new Exception("The currently selected tariff value is not in a valid format. Please remove any characters from the value (R,spaces, etc), This value may only contain numeric digits");
            }

            if(env.ObjVerEx.HasValue(Configuration.DiscountPercentage) && env.ObjVerEx.HasValue(Configuration.RoomTariff)
                && env.ObjVerEx.GetProperty(Configuration.DiscountPercentage).GetValue<double>() != 0)
            {
                double discountPerc = env.ObjVerEx.GetProperty(Configuration.DiscountPercentage).GetValue<double>();

                double discountValue = tariff * discountPerc / 100;

                env.ObjVerEx.SaveProperty(Configuration.DiscountRandValue, MFDataType.MFDatatypeFloating, discountValue);
            }
        }
    }
}
