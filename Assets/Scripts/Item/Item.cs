using UnityEngine;

public enum ItemType
{
    Key,
    Tool,
    Collectible
}

public class Item
{
    public string ItemName;
    public ItemType Type;

    public Item(string itemName, ItemType type)
    {
        ItemName = itemName;
        Type = type;
    }
}

