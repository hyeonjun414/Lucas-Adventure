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
    public int itemCount;
    public int itemValue;

    public Item(ItemType _itype, string _iName, int _iCount)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = _iCount;
        itemInfo = "";
        itemValue = 0;

    }

    public Item(ItemType _itype, string _iName, int _iCount, int _iValue, string _iInfo)
    {
        itemType = _itype;
        itemName = _iName;
        itemCount = _iCount;
        itemInfo = _iInfo;
        itemValue = _iValue;
    }
}
