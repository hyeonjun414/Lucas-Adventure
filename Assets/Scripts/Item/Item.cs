using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Unique,
    Weapon,
    posion,
    coin
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public Item(ItemType _itype, string _iName, Sprite _iImage)
    {
        itemType = _itype;
        itemName = _iName;
        itemImage = _iImage;
    }

    public Item()
    {

    }
}
