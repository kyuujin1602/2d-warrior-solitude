using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    public Inventory_Player inventory;
    public List<Inventory_Item> materialStash;

    public bool CanCraftItem(Inventory_Item itemToCraft)
    {
        return (HasEnoughMaterials(itemToCraft) && inventory.CanAddItem(itemToCraft));
    }

    public void CraftItem(Inventory_Item itemToCraft)
    {
        ConsumeMaterials(itemToCraft);
        inventory.AddItem(itemToCraft);
    }

    public void ConsumeMaterials(Inventory_Item itemToCraft)
    {
        foreach (var requiredItem in itemToCraft.itemData.materials)
        {
            int amountToConsume = requiredItem.stackSize;
            amountToConsume -= ConsumedMaterialsCount(inventory.itemList, requiredItem);

            if (amountToConsume > 0)
                amountToConsume -= ConsumedMaterialsCount(itemList, requiredItem);

            if (amountToConsume > 0)
                amountToConsume -= ConsumedMaterialsCount(materialStash, requiredItem);
        }
    }

    private int ConsumedMaterialsCount(List<Inventory_Item> itemList, Inventory_Item neededItem)
    {
        int amountNeeded = neededItem.stackSize;
        int consumedCount = 0;

        foreach (var item in itemList)
        {
            if (item.itemData != neededItem.itemData)
                continue;

            int removeCount = Mathf.Min(item.stackSize, amountNeeded - consumedCount);

            item.stackSize -= removeCount;
            consumedCount += removeCount;

            if (item.stackSize <= 0)
            {
                itemList.Remove(item);
            }

            if (consumedCount >= amountNeeded)
                break;
        }

        return consumedCount;
    }


    public bool HasEnoughMaterials(Inventory_Item itemToCraft)
    {
        foreach (var requiredItem in itemToCraft.itemData.materials)
        {
            int availableAmount = GetAvailableAmount(requiredItem.itemData);
            if (availableAmount < requiredItem.stackSize)
                return false;
        }
        return true;
    }

    public int GetAvailableAmount(ItemDataSO itemToGet)
    {
        int amount = 0;
        foreach (var item in inventory.itemList)
        {
            if (item.itemData == itemToGet)
                amount += item.stackSize;
        }

        foreach (var item in itemList)
        {
            if (item.itemData == itemToGet)
                amount += item.stackSize;
        }

        foreach(var item in materialStash)
        {
            if (item.itemData == itemToGet)
                amount += item.stackSize;
        }

        return amount;
    }

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
            var newItem = new Inventory_Item(itemToAdd.itemData);
            materialStash.Add(newItem);
        }
        TriggerUpdateUI();
        materialStash = materialStash.OrderBy(item => item.itemData.itemName).ToList();
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
