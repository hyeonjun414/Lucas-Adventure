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

    public int Coin;
    public int maxCoin = 9999;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //기본 장비 지급
        Item baseItem = new Item(ItemType.Weapon, "regular_sword", 999999, 
                                 Resources.Load<Sprite>("ItemImage/" + "regular_sword"));
        AddItem(baseItem);
        Coin = 500;
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
    }
    

}
