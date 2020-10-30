using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> itemScripts = new List<Item>();
    public List<Item> equipWeapon = new List<Item>();
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //기본 장비 지급
        Item baseItem = new Item(ItemType.Weapon, "knife", 999999, 
                                 Resources.Load<Sprite>("ItemImage/" + "knife"));
        AddItem(baseItem);
    }
    private void Update()
    {

    }
    public bool AddItem(Item _item)
    {
        if (_item.itemType == ItemType.Weapon)
        {
            equipWeapon.Add(_item);
            return true;
        }
        else if(_item.itemType == ItemType.Unique)
        {
            itemScripts.Add(_item);
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
