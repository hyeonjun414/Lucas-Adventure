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
public class Unique : MonoBehaviour
{
    public static Unique instance;
    Player player;
    Inventory inven;
    
    public List<bool> uniqueList = new List<bool>();
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = GetComponentInParent<Player>();
        inven = GetComponent<Inventory>();
        for(int i=0; i<4; i++)
        {
            uniqueList.Add(false);
        }
    }
    private void Update()
    {
        checkUnique();


    }
    public void SaveUnique()
    {
        SaveManager.Save(this);
    }
    public void LoadUnique()
    {
        SaveData data = SaveManager.Load_unique();
        uniqueList = data.uniqueList;
    }
    public void checkUnique()
    {
        for(int i=0; i<inven.uniqueitems.Count; i++)
        {
            switch (inven.uniqueitems[i].itemCount)
            {
                case 0:
                    if(uniqueList[0] == false)
                    {
                        HealthUp(100);
                    }
                    uniqueList[0] = true;
                    break;
                case 1:
                    if (uniqueList[1] == false)
                    {
                        DamageUp(10);
                    }
                    uniqueList[1] = true;
                    break;
                case 2:
                    if (uniqueList[2] == false)
                    {
                        ArmorUp(5);
                    }
                    uniqueList[2] = true;
                    break;
                case 3:
                    if (uniqueList[3] == false)
                    {
                        SpeedUp(5);
                    }
                    uniqueList[3] = true;
                    break;
            }
        }
    }

    void HealthUp(int value)
    {
        player.health += value;
        player.maxhealth += value;
    }
    void DamageUp(int value)
    {
        player.damage += value;
        player.equipWeaponto.damage += value;
    }
    void ArmorUp(int value)
    {
        player.armor += value;

    }
    void SpeedUp(int value)
    {
        player.maxSpeed += value;
    }
}
