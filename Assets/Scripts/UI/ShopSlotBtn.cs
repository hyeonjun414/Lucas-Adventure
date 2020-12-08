using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlotBtn : MonoBehaviour
{
    public BTNSlot type;
    public int SlotNum;
    public Shop shop;
    public void OnBtnClick()
    {
        switch (type)
        {
            case BTNSlot.Unique:
                shop.UniqueInfo(SlotNum);
                break;
            case BTNSlot.Potion:
                shop.PotionInfo(SlotNum);
                break;
        }
    }
}
