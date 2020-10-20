using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Posion/Health")]
public class ItemHealEft : ItemEffect
{
    public Player player;
    public int HealingPoint = 0;
    public override bool ExecuteRole(Item getItem, int slotnum)
    {
            player = FindObjectOfType<Player>();
            Debug.Log(player.health);
            GameObject temp1 = player.transform.Find("PlayerGFX").gameObject;
            GameObject temp2 = temp1.transform.Find("Arm L").gameObject;
            for (int i = 2; i < player.hasWeapons.Length; i++)
            {
                if (!player.hasWeapons[i])
                {
                    player.weapons[i] = getItem;
                    player.hasWeapons[i] = true;
                    return true;
                }
            }
            return false;
    }
}
