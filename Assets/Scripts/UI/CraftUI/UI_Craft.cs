using UnityEngine;

/// <summary>
/// Manages the crafting user interface, including initialization and configuration of craft list slots and category
/// buttons.
/// </summary>
public class UI_Craft : MonoBehaviour
{
    [SerializeField] private UI_ItemSlotParent inventoryParent;

    private Inventory_Player inventory;
    private UI_CraftListSlot[] craftListSlots; // Array of craft list slots in the UI
    private UI_CraftableCategoryBtn[] craftListButtons; // Array of craft categories buttons in the UI
    private UI_CraftPreview craftPreview; // Reference to the craft preview component

    private void SetupCraftCategoriesButton()
    {
        craftListSlots = GetComponentsInChildren<UI_CraftListSlot>();
        foreach (var slot in craftListSlots)
        {
            slot.gameObject.SetActive(false); // Deactivate all craft list slots initially
        }

        craftListButtons = GetComponentsInChildren<UI_CraftableCategoryBtn>();
        foreach (var button in craftListButtons)
        {
            button.SetupCraftSlots(craftListSlots); // Setup each craft category button
        }
    }

    public void SetupCraftUI(Inventory_Storage storage)
    {
        inventory = storage.inventory;
        inventory.OnInventoryChange += UpdateUI; // Subscribe to inventory change event to update the UI
        UpdateUI(); // Initial update of the UI
        craftPreview = GetComponentInChildren<UI_CraftPreview>();
        craftPreview.SetupPreviewSlots(storage); // Setup the craft preview slots with the player's storage
        SetupCraftCategoriesButton();
    }

    private void UpdateUI() => inventoryParent.UpdateSlots(inventory.itemList); // Update the inventory slots in the UI
}
