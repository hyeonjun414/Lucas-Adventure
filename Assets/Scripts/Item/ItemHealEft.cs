using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Posion/Health")]
public class ItemHealEft : ItemEffect
{
    public int HealingPoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp ass :" + HealingPoint);
        return true;
    }
}
