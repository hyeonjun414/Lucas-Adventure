using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<Item> items = new List<Item>();
    public List<Item> unique = new List<Item>();

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //특수 아이템
        UniqueAdd(ItemType.Unique, "healthUp");



        //ShotSword
        WeaponAdd(ItemType.Weapon, "machete", 20);

        //Sword
        WeaponAdd(ItemType.Weapon, "duel_sword", 10);
        WeaponAdd(ItemType.Weapon, "golden_sword", 10);
        WeaponAdd(ItemType.Weapon, "katana", 10);
        WeaponAdd(ItemType.Weapon, "red_gem_sword", 10);
        WeaponAdd(ItemType.Weapon, "regular_sword", 10);
        WeaponAdd(ItemType.Weapon, "rusty_sword", 10);

        //Mace
        WeaponAdd(ItemType.Weapon, "baton_with_spike", 7);
        WeaponAdd(ItemType.Weapon, "big_hammer", 7);
        WeaponAdd(ItemType.Weapon, "axe", 7);
        WeaponAdd(ItemType.Weapon, "hammer", 7);
        WeaponAdd(ItemType.Weapon, "mace", 7);

        //Spear
        WeaponAdd(ItemType.Weapon, "spear", 15);
        WeaponAdd(ItemType.Weapon, "stone_spear", 15);

        //HeavyWeapon
        WeaponAdd(ItemType.Weapon, "sword", 5);
        WeaponAdd(ItemType.Weapon, "knight_sword", 5);
        WeaponAdd(ItemType.Weapon, "lavish_sword", 5);
        WeaponAdd(ItemType.Weapon, "sword", 5);
        WeaponAdd(ItemType.Weapon, "cleaver", 5);



    }
    void WeaponAdd(ItemType itype, string iName, int iCount)
    {
        items.Add(new Item(itype, iName, iCount,Resources.Load<Sprite>("ItemImage/" + iName)));
    }

    void UniqueAdd(ItemType itype, string iName)
    {
        unique.Add(new Item(itype, iName, 0, Resources.Load<Sprite>("ItemImage/" + iName)));
    }

}
