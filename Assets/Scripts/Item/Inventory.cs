using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> uniqueitems = new List<Item>();
    public List<Item> equipWeapon = new List<Item>();
    public int maxWeapon = 4;
    public int maxUnique = 8;

    public int curCoin;
    public int maxCoin = 9999;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //기본 장비 지급
        Item baseItem = new Item(ItemType.Weapon, "regular_sword", 999999);
        AddItem(baseItem);
        curCoin = 500;
    }
    public void SaveInven()
    {
        SaveManager.Save(this);
    }

    public void LoadInven()
    {
        SaveData inven = SaveManager.Load_inven();
        uniqueitems = inven.uniqueitems;
        curCoin = inven.curCoin;
        equipWeapon = inven.equipWeapon;
    }
    public bool AddItem(Item _item)
    {
        if (_item.itemType == ItemType.Weapon)
        {
            if(equipWeapon.Count == maxWeapon) return false;
            equipWeapon.Add(_item);
            return true;
        }
        else if(_item.itemType == ItemType.Unique)
        {
            if(uniqueitems.Count == maxUnique) return false;
            uniqueitems.Add(_item);
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FieldItem"))
        {
           FieldItem fieldItems = other.GetComponent<FieldItem>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }
        }
        else if (other.CompareTag("Coin"))
        {
            Coin coins = other.GetComponent<Coin>();
            curCoin += coins.value;
            coins.picked();
            
        }
    }
}
