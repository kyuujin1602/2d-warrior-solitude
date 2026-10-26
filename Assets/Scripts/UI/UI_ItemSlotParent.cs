using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UI_ItemSlotParent : MonoBehaviour
{
    private UI_ItemSlot[] itemSlots;

    public void UpdateSlots(List<Inventory_Item> itemList)
    {
        if (itemSlots == null)
        {
            itemSlots = GetComponentsInChildren<UI_ItemSlot>();
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < itemList.Count)
            {
                itemSlots[i].UpdateSlot(itemList[i]);
            }
            else 
                itemSlots[i].UpdateSlot(null);
        }
    }
}
