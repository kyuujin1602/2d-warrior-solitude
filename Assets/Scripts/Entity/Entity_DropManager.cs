using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class Entity_DropManager : MonoBehaviour
{
    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private ItemListDataSO dropData;

    [Header("Drop restrictions")]
    [SerializeField] private int maxRarityAmount = 1200;
    [SerializeField] private int maxItemsToDrop = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DropItems();        
    }

    public virtual void DropItems()
    {
        if (dropData == null)
        {
            Debug.LogWarning("Drop data is not assigned on entity " + gameObject.name);
            return;
        }

        List<ItemDataSO> drops = RollDrops();
        int amountToDrop = Mathf.Min(drops.Count, maxItemsToDrop);
        for (int i = 0; i < amountToDrop; i++)
        {
            CreateItemDrop(drops[i]);
        }
    }

    protected void CreateItemDrop(ItemDataSO drop)
    {
        GameObject newItem = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        newItem.GetComponent<Object_ItemPickup>().SetupItem(drop);
    }

    public List<ItemDataSO> RollDrops()
    {
        List<ItemDataSO> possibleDrops = new List<ItemDataSO>();
        List<ItemDataSO> finalDrops = new List<ItemDataSO>();
        float maxRarityAmount = this.maxRarityAmount;

        foreach (var item in dropData.itemList)
        {
            float dropChance= item.GetDropChance();
            if (Random.Range(0, 100) <= dropChance)
                possibleDrops.Add(item);
        }

        // Sort drops by rarity
        possibleDrops = possibleDrops.OrderByDescending(item => item.itemRarity).ToList();


        // Add to pool until rarity limit
        foreach (var item in possibleDrops)
        {
            if (maxRarityAmount > item.itemRarity)
            {
                finalDrops.Add(item);
                maxRarityAmount -= item.itemRarity;
            }
        }
        return finalDrops;
    }

}
