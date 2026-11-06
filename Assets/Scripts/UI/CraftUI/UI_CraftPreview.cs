using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftPreview : MonoBehaviour
{
    private Inventory_Item itemToCraft;
    private Inventory_Storage playerStorage;
    private UI_CraftPreviewSlot[] recipeSlots;

    [Header("Craft Preview Components")]

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    public void SetupPreviewSlots(Inventory_Storage storage)
    {
        this.playerStorage = storage;
        recipeSlots = GetComponentsInChildren<UI_CraftPreviewSlot>();
        
        foreach (var slot in recipeSlots)
            slot.gameObject.SetActive(false);
    }

    public void UpdateCraftPreview(ItemDataSO craftData)
    {
        itemToCraft = new Inventory_Item(craftData);
        this.itemIcon.sprite = itemToCraft.itemData.itemIcon;
        this.itemName.text = itemToCraft.itemData.itemName;
        this.itemDescription.text = itemToCraft.GetItemInfo();
        UpdateCraftPreviewSlots();
    }

    private void UpdateCraftPreviewSlots()
    {
        foreach (var slot in recipeSlots)
            slot.gameObject.SetActive(false);

        for (int i = 0; i < itemToCraft.itemData.materials.Length; i++)
        {
            Inventory_Item requiredItem = itemToCraft.itemData.materials[i];
            int availableAmount = playerStorage.GetAvailableAmount(requiredItem.itemData);
            int requiredAmount = requiredItem.stackSize;

            recipeSlots[i].gameObject.SetActive(true);
            recipeSlots[i].SetupMaterialSlot(requiredItem.itemData, availableAmount, requiredAmount);
        }

        Debug.Log("Finished updating craft preview slots for " + itemToCraft.itemData.itemName);
    }

    public void ConfirmCraft()
    {
        if (itemToCraft == null)
            return;

        if (playerStorage.CanCraftItem(itemToCraft))
            playerStorage.CraftItem(itemToCraft);
        UpdateCraftPreviewSlots();
    }
}
