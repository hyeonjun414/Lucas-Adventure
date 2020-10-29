using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Add(ItemType.Weapon, "axe");
        Add(ItemType.Weapon, "hammer");
        Add(ItemType.Weapon, "sword");
    }
    void Add(ItemType itype, string iName)
    {
        items.Add(new Item(itype, iName, Resources.Load<Sprite>("ItemImage/" + iName)));
    }

}
