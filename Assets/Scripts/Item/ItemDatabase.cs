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
        Add(ItemType.Weapon, "axe", 2);
        Add(ItemType.Weapon, "hammer",2);
        Add(ItemType.Weapon, "sword",2);
    }
    void Add(ItemType itype, string iName, int iCount)
    {
        items.Add(new Item(itype, iName, iCount,Resources.Load<Sprite>("ItemImage/" + iName)));
    }

}
