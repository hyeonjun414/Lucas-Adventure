﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    posion,
    coin,
    Etc
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public ItemEffect eft;

    public bool Use()
    {
        bool isUsed = false;
        isUsed = eft.ExecuteRole();
        isUsed = true;
        return isUsed;
    }
}
