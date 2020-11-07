using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UniqueName
{
    healthUp,
    DamageUp,
    ArmorUp,
    SpeedUp,
};
public class uniquelist
{
    public int itemindex;
    public bool check;
    public uniquelist(int _itemindex, bool _check)
    {
        itemindex = _itemindex;
        check = _check;
    }
}
public class Unique : MonoBehaviour
{

    Player player;
    Inventory inven;
    
    public List<uniquelist> uniqueList = new List<uniquelist>();
    private void Start()
    {
        player = GetComponentInParent<Player>();
        inven = GetComponent<Inventory>();
        for(int i=0; i<4; i++)
        {
            uniqueList.Add(new uniquelist(-1, false));
        }
    }
    private void Update()
    {
        checkUnique();


    }
    public void checkUnique()
    {
        for(int i=0; i<inven.uniqueitems.Count; i++)
        {
            switch (inven.uniqueitems[i].itemCount)
            {
                case 0:
                    uniqueList[0].itemindex = i;
                    if(uniqueList[0].check == false)
                    {
                        healthUp(100);
                    }
                    uniqueList[0].check = true;
                    break;
                case 1:
                    uniqueList[1].itemindex = i;
                    if (uniqueList[1].check == false)
                    {
                        healthUp(100);
                    }
                    uniqueList[1].check = true;
                    break;
                case 2:
                    uniqueList[2].itemindex = i;
                    if (uniqueList[2].check == false)
                    {
                        healthUp(100);
                    }
                    uniqueList[2].check = true;
                    break;
                case 3:
                    uniqueList[3].itemindex = i;
                    if (uniqueList[3].check == false)
                    {
                        healthUp(100);
                    }
                    uniqueList[3].check = true;
                    break;
            }
        }
    }

    void healthUp(int value)
    {
        player.health += value;
        player.maxhealth += value;
    }
    void DamageUp()
    {

    }
    void ArmorUp()
    {

    }
    void SpeedUp()
    {

    }
}
