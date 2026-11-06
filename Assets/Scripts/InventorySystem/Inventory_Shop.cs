using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory_Shop : Inventory_Base
{
    private Inventory_Player playerInventory;

    [SerializeField] private int minItemAmount = 4;
    [SerializeField] private ItemListDataSO shopData;

    protected override void Awake()
    {
        base.Awake();
        FillShopList();
    }

    public void TryBuyItem(Inventory_Item itemToBuy, bool buyFullStack)
    {
        int amountToBuy = buyFullStack ? itemToBuy.stackSize : 1;

        for (int i = 0; i < amountToBuy; i++)
        {
            if (itemToBuy.buyPrice > playerInventory.gold)
            {
                // Not enough money for 1 item
                Debug.Log("Not enough money!");
                return;
            }
            
            if (itemToBuy.itemData.itemType == ItemType.Material)
            {
                playerInventory.storage.AddMaterialToStash(itemToBuy);
            }
            else
            {
                if (playerInventory.CanAddItem(itemToBuy))
                {
                    var itemToAdd = new Inventory_Item(itemToBuy.itemData);
                    playerInventory.AddItem(itemToAdd);
                }
            }
            playerInventory.gold -= itemToBuy.buyPrice;
            RemoveOneItem(itemToBuy);
        }
        Debug.Log("Bought " + amountToBuy + " of " + itemToBuy.itemData.itemName);
        TriggerUpdateUI();
    }

    public void TrySellItem(Inventory_Item itemToSell, bool sellFullStack)
    {
        int amountToSell = sellFullStack ? itemToSell.stackSize : 1;
        for (int i = 0; i < amountToSell; i++)
        {
            int sellPrice = Mathf.FloorToInt(itemToSell.sellPrice);
            playerInventory.gold += sellPrice;
            playerInventory.RemoveOneItem(itemToSell);
        }
        TriggerUpdateUI();
    }

    public void FillShopList()
    {
        itemList.Clear();
        List<Inventory_Item> possibleItems = new List<Inventory_Item>();

        foreach (var itemData in shopData.itemList)
        {
            int randomizedStack = Random.Range(itemData.minStackSizeAtShop, itemData.maxStackSizeAtShop + 1);
            int finalStackSize = Mathf.Clamp(randomizedStack, 1, itemData.maxStackSize);

            Inventory_Item newItem = new Inventory_Item(itemData);
            newItem.stackSize = finalStackSize;

            possibleItems.Add(newItem);
        }

        int randomItemCount = Random.Range(minItemAmount, maxInventorySize + 1);
        int finalItemCount = Mathf.Clamp(randomItemCount, 1, possibleItems.Count);

        for (int i = 0; i < finalItemCount; i++)
        {
            int randomIndex = Random.Range(0, possibleItems.Count);
            var selectedItem = possibleItems[randomIndex];

            if (CanAddItem(selectedItem))
            {
                possibleItems.Remove(selectedItem); // Remove from the pool to avoid duplicates
                AddItem(selectedItem);
            }
            
        }
        TriggerUpdateUI();
    }

    public void SetPlayerInventory(Inventory_Player inventory) => playerInventory = inventory;
}
