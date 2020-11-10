using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Unique,
    Weapon,
    Potion,
    Coin
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public string itemInfo;
    public Sprite itemImage;
    public int itemCount;
    public int itemValue;

    public Item(ItemType _itype, string _iName, int _iCount, Sprite _iImage)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = _iCount;
        itemImage = _iImage;
        itemInfo = "";
        itemValue = 0;

    }

    public Item(ItemType _itype, string _iName, int _iCount, int _iValue, Sprite _iImage, string _iInfo)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = _iCount;
        itemImage = _iImage;
        itemInfo = _iInfo;
        itemValue = _iValue;
    }
}
