using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StorageSlot : UI_ItemSlot
{
    private Inventory_Storage storage;
    public enum StorageSlotType
    {
        Storage,
        PlayerInventory,
        None
    }

    public StorageSlotType Type;

    public void SetStorage(Inventory_Storage storage) => this.storage = storage;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null || Type == StorageSlotType.None) return; // Unclickable if empty or item is Material or others

        bool transferStack = Input.GetKey(KeyCode.LeftControl);

        if (Type == StorageSlotType.Storage)
        {
            storage.TransferFromStorageToPlayer(itemInSlot, transferStack);
        }
        else if (Type == StorageSlotType.PlayerInventory)
        {
            storage.TransferFromPlayerToStorage(itemInSlot, transferStack);
        }    
        ui.itemToolTip.ShowToolTip(false, null);
    }
}
