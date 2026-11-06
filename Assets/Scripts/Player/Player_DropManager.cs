using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Player_DropManager : Entity_DropManager
{
    [Header("Drop details")]
    [SerializeField] private float chanceToLoseItem = 90f;

    private Inventory_Player inventory;

    public override void DropItems()
    {
        List<Inventory_Item> inventoryCopy = new List<Inventory_Item>(inventory.itemList);
        List<Inventory_EquipmentSlot> equipmentCopy = new List<Inventory_EquipmentSlot>(inventory.equipList);

        foreach (var item in inventoryCopy)
        {
            if (Random.Range(0f, 100f) <= chanceToLoseItem)
            {
                CreateItemDrop(item.itemData);
                inventory.RemoveStack(item);
            }
        }

        foreach (var equip in equipmentCopy)
        {
            if (Random.Range(0f, 100f) <= chanceToLoseItem && equip.HasItem())
            {
                var item = equip.GetEquipedItem();
                CreateItemDrop(item.itemData);
                inventory.UnequipItem(item);
                inventory.RemoveStack(item);
            }
        }
    }
}
