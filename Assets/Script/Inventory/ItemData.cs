using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int maxStackSize;
    public Sprite icon;
    public GameObject worldPrefab;
}
