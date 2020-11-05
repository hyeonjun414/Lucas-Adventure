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
    public int itemCount;

    public Item(ItemType _itype, string _iName, int _iCount, Sprite _iImage)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = _iCount;
        itemImage = _iImage;
    }

    public Item(ItemType _itype, string _iName, Sprite _iImage)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = -1;
        itemImage = _iImage;
    }
}
