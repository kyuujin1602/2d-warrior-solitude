using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class representing a UI slots of the craftable items that have shown on the list.
/// </summary>
public class UI_CraftListSlot : MonoBehaviour
{
    private ItemDataSO itemToCraft;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    [SerializeField] private UI_CraftPreview craftPreview;

    /// <summary>
    /// Showing the item icon and name of the craftable item on the UI slot.
    /// </summary>
    /// <param name="craftData"></param>
    public void SetupButton(ItemDataSO craftData)
    {
        this.itemToCraft = craftData;
        itemIcon.sprite = craftData.itemIcon;
        itemName.text = craftData.itemName;
    }

    public void UpdateCraftPreview() => craftPreview.UpdateCraftPreview(itemToCraft);
}
