using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Weapon/Equip")]
public class ItemWeaponEft : ItemEffect
{
    public Player player;
    public Item thisitem;
    public override bool ExecuteRole()
    {
        
        player = FindObjectOfType<Player>();
        Debug.Log(player.health);
        if (!player.hasWeapons[0])
        {
            player.weapons[0] = Resources.Load<GameObject>("Prefabs/items/Weapon/" +thisitem.itemName);
            return true;
        }
        else if (!player.hasWeapons[1])
        {
            player.weapons[1] = Resources.Load<GameObject>("Prefabs/items/Weapon/" + thisitem.itemName);
            return true;
        }
        return false;
    }
}
