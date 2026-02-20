using System;
using System.Collections.Generic;
using System.Threading;
using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;
namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.Resident")]
        public void BeforeCheckInRoomChanges(EventHandlerEnvironment env)
        {
            var residentName = env.ObjVerEx.GetProperty(Configuration.Resident_ResidentDetail).GetValueAsLocalizedText();

            var currentRoomLookup = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();
            var isResidentRoomSwap = env.ObjVerEx.HasValue(Configuration.Resident_RoomSwap);
            var holdRoom = env.ObjVerEx.GetPropertyAsBoolean(Configuration.Resident_HoldRoom) ?? false;
            var hasHeldRoom = env.ObjVerEx.HasValue(Configuration.Resident_HeldRoom);

            if (isResidentRoomSwap)
            {

                if (hasHeldRoom)
                {
                    throw new Exception("You cannot perform a room swap while there is a held room. Please clear the held room before performing a room swap.");
                }

                var residentRoomSwap = env.ObjVerEx.GetProperty(Configuration.Resident_RoomSwap).TypedValue.GetValueAsLookup();
                ObjVerEx residentRoomSwapObjVerEx = new ObjVerEx(env.Vault, residentRoomSwap);

                var residentRoomSwapCurrentRoomLookup = residentRoomSwapObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

                env.ObjVerEx.SaveProperty(Configuration.CurrentRoom, MFDataType.MFDatatypeLookup, residentRoomSwapCurrentRoomLookup);
                residentRoomSwapObjVerEx.SaveProperty(Configuration.CurrentRoom, MFDataType.MFDatatypeLookup, currentRoomLookup);
            }

            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            foreach (PropertyValueChange change in changes.Changed)
            {
                if (change.PropertyDef == Configuration.Active.ID && change.ChangeType == PropertyValueChangeType.Modified && env.ObjVerEx.HasValue(Configuration.CurrentRoom))
                {
                    SetRoomVacantWhenInactive(env);
                }

                if (change.PropertyDef == Configuration.RoomTariff.ID && change.ChangeType == PropertyValueChangeType.Modified && env.ObjVerEx.HasValue(Configuration.CurrentRoom))
                {
                    SetDiscountValueIfPercentage(env);
                }

                if (change.PropertyDef == Configuration.CurrentRoom.ID && change.ChangeType == PropertyValueChangeType.Modified && env.ObjVerEx.HasValue(Configuration.CurrentRoom))
                {
                    if (hasHeldRoom)
                    {
                        throw new Exception($"You cannot change the room while there is a held room on {residentName}. Please clear the held room before changing the current room.");
                    }

                    if (!change.OldValue.TypedValue.IsNULL())
                    {
                        var oldRoomLookup = change.OldValue.TypedValue.GetValueAsLookup();

                        if (isResidentRoomSwap)
                        {
                            //Placeholder
                        }
                        else if (holdRoom)
                        {
                            env.ObjVerEx.SaveProperty(Configuration.Resident_HeldRoom, MFDataType.MFDatatypeLookup, oldRoomLookup);
                        }
                        else
                        {
                            ObjVerEx oldRoom = new ObjVerEx(env.Vault, oldRoomLookup);
                            if (!oldRoom.IsDeleted)
                            {
                                SetRoomVacancy(oldRoom, env.Vault, true); //SetRoomVacancy old room vacant
                            }
                        }
                    }

                    SetRoomNotVacant(env.ObjVerEx, env.Vault);
                    UpdateRoomTariffOnRoomChange(env);
                    SetDiscountValueIfPercentage(env);
                }
            }

            if (!holdRoom && hasHeldRoom)
            {
                ObjVerEx currentRoom = new ObjVerEx(env.Vault, currentRoomLookup);

                if (!currentRoom.IsDeleted)
                {
                    SetRoomVacancy(currentRoom, env.Vault, true); //SetRoomVacancy old room vacant
                }

                var heldRoomLookup = env.ObjVerEx.GetProperty(Configuration.Resident_HeldRoom).TypedValue.GetValueAsLookup();
                env.ObjVerEx.SetProperty(Configuration.CurrentRoom, MFDataType.MFDatatypeLookup, heldRoomLookup);
                env.ObjVerEx.SetProperty(Configuration.Resident_HeldRoom, MFDataType.MFDatatypeLookup, null);
                env.ObjVerEx.SaveProperties();
            }
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.Resident")]
        public void MFEventHandlerAfterCheckInChangesFinalize(EventHandlerEnvironment env)
        {
            var hasResidentRoomSwap = env.ObjVerEx.HasValue(Configuration.Resident_RoomSwap);

            if (hasResidentRoomSwap)
            {
                Thread.Sleep(1200); //We need this if the ReportingConfigurator export does not handle files generated in the same second
                env.ObjVerEx.SaveProperty(Configuration.Resident_RoomSwap, MFDataType.MFDatatypeLookup, null);
            }
        }

        public void UpdateRoomTariffOnRoomChange(EventHandlerEnvironment env)
        {
            Lookup roomLookup = env.ObjVerEx.GetProperty(Configuration.CurrentRoom).TypedValue.GetValueAsLookup();

            ObjVerEx room = new ObjVerEx(env.Vault, roomLookup);
            Lookup selectedTariff = room.GetProperty(Configuration.RoomTariff).TypedValue.GetValueAsLookup();

            if (selectedTariff != null)
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
            if (!double.TryParse(env.ObjVerEx.GetProperty(Configuration.RoomTariff).GetValueAsLocalizedText(), out tariff))
            {
                throw new Exception("The currently selected tariff value is not in a valid format. Please remove any characters from the value (R,spaces, etc), This value may only contain numeric digits");
            }

            if (env.ObjVerEx.HasValue(Configuration.DiscountPercentage) && env.ObjVerEx.HasValue(Configuration.RoomTariff)
                && env.ObjVerEx.GetProperty(Configuration.DiscountPercentage).GetValue<double>() != 0)
            {
                double discountPerc = env.ObjVerEx.GetProperty(Configuration.DiscountPercentage).GetValue<double>();

                double discountValue = tariff * discountPerc / 100;

                env.ObjVerEx.SaveProperty(Configuration.DiscountRandValue, MFDataType.MFDatatypeFloating, discountValue);
            }
        }
    }
}
