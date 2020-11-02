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
        //ShotSword
        Add(ItemType.Weapon, "machete", 20);

        //Sword
        Add(ItemType.Weapon, "duel_sword", 10);
        Add(ItemType.Weapon, "golden_sword", 10);
        Add(ItemType.Weapon, "katana", 10);
        Add(ItemType.Weapon, "red_gem_sword", 10);
        Add(ItemType.Weapon, "regular_sword", 10);
        Add(ItemType.Weapon, "rusty_sword", 10);
        
        //Mace
        Add(ItemType.Weapon, "batom_with_spike", 7);
        Add(ItemType.Weapon, "big_hammer", 7);
        Add(ItemType.Weapon, "axe", 7);
        Add(ItemType.Weapon, "hammer", 7);
        Add(ItemType.Weapon, "mace", 7);

        //Spear
        Add(ItemType.Weapon, "spear", 15);
        Add(ItemType.Weapon, "stone_spear", 15);

        //HeavyWeapon
        Add(ItemType.Weapon, "sword", 5);
        Add(ItemType.Weapon, "knight_sword", 5);
        Add(ItemType.Weapon, "lavish_sword", 5);
        Add(ItemType.Weapon, "sword", 5);
        Add(ItemType.Weapon, "cleaver", 5);



    }
    void Add(ItemType itype, string iName, int iCount)
    {
        items.Add(new Item(itype, iName, iCount,Resources.Load<Sprite>("ItemImage/" + iName)));
    }

}
