using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Weapon/Equip")]
public class ItemWeaponEft : ItemEffect
{
    public Player player;
    public override bool ExecuteRole(string itemName)
    {
        
        player = FindObjectOfType<Player>();
        Debug.Log(player.health);
        GameObject temp1 = player.transform.Find("PlayerGFX").gameObject;
        GameObject temp2 = temp1.transform.Find("Arm L").gameObject;
        for (int i=0; i<player.hasWeapons.Length; i++)
        {
            if (!player.hasWeapons[i])
            {
                GameObject item = Resources.Load<GameObject>("Weapon/" + itemName);
                GameObject go = Instantiate(item,
                                temp2.transform.position,
                                temp2.transform.rotation);
                go.SetActive(false);
                go.transform.parent = temp2.transform;
                player.weapons[i] = go;
                
                
                player.hasWeapons[i] = true;
                return true;
            }
        }
        return false;
    }
}
