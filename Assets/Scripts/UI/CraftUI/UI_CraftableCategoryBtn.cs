using UnityEngine;

/// <summary>
/// Class for showing the list of craftable items for each category in the Craft UI.
/// </summary>
public class UI_CraftableCategoryBtn : MonoBehaviour
{
    [SerializeField] private ItemListDataSO craftListData;
    private UI_CraftListSlot[] craftListSlots;

    public void SetupCraftSlots(UI_CraftListSlot[] slots)
    {
        craftListSlots = slots;
    }

    public void UpdateCraftSlots()
    {
        if (craftListData == null || craftListSlots == null) return;

        foreach (var slot in craftListSlots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < craftListData.itemList.Length; i++)
        {
            ItemDataSO itemData = craftListData.itemList[i];
            craftListSlots[i].gameObject.SetActive(true);
            // Craft Slot Button Setup with item data
            craftListSlots[i].SetupButton(itemData);
        }
    }
}
