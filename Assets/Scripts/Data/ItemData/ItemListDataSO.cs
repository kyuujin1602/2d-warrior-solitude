using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item List", fileName = "Item List data - ")]
public class ItemListDataSO : ScriptableObject
{
    public ItemDataSO[] itemList;
}
