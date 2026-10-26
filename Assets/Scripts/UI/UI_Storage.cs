using UnityEngine;

public class UI_Storage : MonoBehaviour
{
    [SerializeField] private UI_ItemSlotParent inventorySlotParent;
    [SerializeField] private UI_ItemSlotParent storageSlotParent;
    [SerializeField] private UI_ItemSlotParent materialSlotParent;

    private Inventory_Player inventory;
    private Inventory_Storage storage;

    public void SetupStorage(Inventory_Player inventory, Inventory_Storage storage)
    {
        this.inventory = inventory;
        this.storage = storage;
        storage.OnInventoryChange += UpdateUI;
        UpdateUI();
        UI_StorageSlot[] storageSlots = GetComponentsInChildren<UI_StorageSlot>();

        foreach (var slot in storageSlots)
            slot.SetStorage(storage);

    }
    private void UpdateUI()
    {
        // Update the UI elements based on the current state of the storage and player inventory
        // This could involve refreshing item slots, updating counts, etc.
        inventorySlotParent.UpdateSlots(inventory.itemList);
        storageSlotParent.UpdateSlots(storage.itemList);
        materialSlotParent.UpdateSlots(storage.materialStash);
    }
}
