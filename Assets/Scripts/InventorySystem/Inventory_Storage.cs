using System.Collections.Generic;
using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    private Inventory_Player inventory;
    public List<Inventory_Item> materialStash;

    public Inventory_Item StackableInStash(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = materialStash.FindAll(item => item.itemData == itemToAdd.itemData);
        foreach (Inventory_Item item in stackableItems)
        {
            if (item.CanAddStack())
            {
                return item;
            }
        }
        return null;
    }

    public void AddMaterialToStash(Inventory_Item itemToAdd)
    {
        Inventory_Item stackableItem = StackableInStash(itemToAdd);
        if (stackableItem != null)
        {
            stackableItem.AddStack();
        }
        else
        {
            materialStash.Add(itemToAdd);
        }
        TriggerUpdateUI();
    }

    public void SetInventory(Inventory_Player inventory) => this.inventory = inventory; // Set the inventory reference whenever the storage is called

    public void TransferFromStorageToPlayer(Inventory_Item item, bool transferStack)
    {
        int transferAmount = transferStack ? item.stackSize : 1;

        for (int i = 0; i < transferAmount; i++)
        {
            if (inventory.CanAddItem(item))
            {
                var itemToAdd = new Inventory_Item(item.itemData);
                RemoveOneItem(item);
                inventory.AddItem(itemToAdd);
            }
        }

        TriggerUpdateUI();
    }

    public void TransferFromPlayerToStorage(Inventory_Item item, bool transferStack)
    {
        int transferAmount = transferStack ? item.stackSize : 1;

        for (int i = 0; i < transferAmount; i++)
        {
            if (CanAddItem(item))
            {
                var itemToAdd = new Inventory_Item(item.itemData);

                inventory.RemoveOneItem(item);
                AddItem(itemToAdd);
            }
        }

        TriggerUpdateUI();
    }
}
