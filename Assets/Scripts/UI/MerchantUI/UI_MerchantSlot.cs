using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MerchantSlot : UI_ItemSlot
{
    Inventory_Shop shopInventory;

    public enum MerchantSlotType
    {
        MerchantSlot,
        PlayerSlot
    }
    public MerchantSlotType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
        {
            Debug.Log("No item in slot");
            return;
        }

        bool rightButtonClicked = eventData.button == PointerEventData.InputButton.Right;
        bool leftButtonClicked = eventData.button == PointerEventData.InputButton.Left;

        if (slotType == MerchantSlotType.PlayerSlot)
        {
            if (rightButtonClicked)
            {
                //sell
                bool sellStackInput = Input.GetKey(KeyCode.LeftControl);
                shopInventory.TrySellItem(itemInSlot, sellStackInput);
            }
            else if (leftButtonClicked)
            {
                base.OnPointerDown(eventData);
            }
        }
        else if (slotType == MerchantSlotType.MerchantSlot)
        {
            if (leftButtonClicked)
                return;
            // Buy
            bool buyStackInput = Input.GetKey(KeyCode.LeftControl);
            shopInventory.TryBuyItem(itemInSlot, buyStackInput);
        }

        ui.itemToolTip.ShowToolTip(false, null);
    }

    public void SetupMerchantUI(Inventory_Shop shop) => shopInventory = shop;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        if (slotType == MerchantSlotType.PlayerSlot)
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, false, true);
        else if (slotType == MerchantSlotType.MerchantSlot)
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, true, true);
    }
}
