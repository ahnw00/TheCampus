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
    public Sprite Icon; // 아이템 이미지 (여기다 해도 되나?)

    public Item(string itemName, ItemType type)
    {
        ItemName = itemName;
        Type = type;
    }
}

