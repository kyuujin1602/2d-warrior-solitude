using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftPreviewSlot : MonoBehaviour
{


    [Header("UI References")]
    [SerializeField] private Image materialIcon;
    [SerializeField] private TextMeshProUGUI materialNameText;
    [SerializeField] private TextMeshProUGUI materialAmountText;

    public void SetupMaterialSlot(ItemDataSO materialData, int availableAmount, int requiredAmount)
    {
        materialIcon.sprite = materialData.itemIcon;
        materialNameText.text = materialData.itemName;
        materialAmountText.text = $"{availableAmount}/{requiredAmount}";
    }
}
