using UnityEngine;

public class UI_Merchant : MonoBehaviour
{
    private Inventory_Shop shopInventory;
    private Inventory_Player playerInventory;

    [SerializeField] private UI_ItemSlotParent shopInventorySlots;
    [SerializeField] private UI_ItemSlotParent playerInventorySlots;
    [SerializeField] private UI_EquipSlotParent playerEquipSlots;

    public void SetupMerchantUI(Inventory_Shop shopInventory, Inventory_Player playerInventory)
    {
        this.shopInventory = shopInventory;
        this.playerInventory = playerInventory;

        this.playerInventory.OnInventoryChange += UpdateSlotUI;
        this.shopInventory.OnInventoryChange += UpdateSlotUI;
        UpdateSlotUI();

        UI_MerchantSlot[] merchantSlots = GetComponentsInChildren<UI_MerchantSlot>();
        foreach (var slot in merchantSlots)
        {
            slot.SetupMerchantUI(shopInventory);
        }
    }

    private void UpdateSlotUI()
    {
        if (playerInventory == null)
            return;

        shopInventorySlots.UpdateSlots(shopInventory.itemList);
        playerInventorySlots.UpdateSlots(playerInventory.itemList);
        playerEquipSlots.UpdateEquipmentSlots(playerInventory.equipList);
    }
}
