using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Weapon/Equip")]
public class ItemWeaponEft : ItemEffect
{
    public Player player;
    public override bool ExecuteRole(Item getItem, int slotnum)
    {
        
        player = FindObjectOfType<Player>();
        Debug.Log(player.health);
        GameObject temp1 = player.transform.Find("PlayerGFX").gameObject;
        GameObject temp2 = temp1.transform.Find("Arm L").gameObject;
        int i = player.isWeapon;
        if (!player.hasWeapons[i])
        {
            GameObject item = Resources.Load<GameObject>("Weapon/" + getItem.itemName);
            GameObject go = Instantiate(item,
                            temp2.transform.position,
                            temp2.transform.rotation);
            go.SetActive(false);
            go.transform.parent = temp2.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            player.weapons[i] = getItem;
                
            player.hasWeapons[i] = true;
            return true;
        }
        //else 
        return false;
    }
}
