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
    public ItemEffect eft;

    public bool Use(Item getItem, int slotnum)
    {
        
        bool isUsed = false;
        if (getItem == null) return isUsed;
        isUsed = eft.ExecuteRole(getItem, slotnum);
        isUsed = true;
        return isUsed;
    }

    public bool SwitchWeapon(Item getItem, int slotnum)
    {

        // 리턴값에 무기정보를 가져가서 기존의 슬롯과 정보를 교환한다.

        bool isSwitch = false;
        return isSwitch;
    }
}
