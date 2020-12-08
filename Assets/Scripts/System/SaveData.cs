using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class SaveData {

    public float Speed; //기본 이동속도
    public float maxSpeed;
    public int damage;
    public int armor;

    public string curMapName;
    public int coin;  //가지고 있는 코인
    public int health;// 현재 체력
    public int maxhealth;// 최대 체력

    public float exp;   // 현재 경험치
    public int level;

    public List<Item> uniqueitems;
    public List<Item> equipWeapon;
    public List<bool> uniqueList;
    public int curCoin;
    public int curArea;

    public SaveData(Player player)
    {
        Speed = player.Speed;
        maxSpeed = player.maxSpeed;
        health = player.health; //hp
        maxhealth = player.maxhealth;
        level = player.level; //level
        damage = player.damage; // damage
        armor = player.armor; //아머
        exp = player.exp; //경험치
        curMapName = player.curMapName; //맵
    }

    public SaveData(Inventory inven)
    {
        uniqueitems = inven.uniqueitems;
        curCoin = inven.curCoin;
        equipWeapon = inven.equipWeapon;
    }
    public SaveData(Unique unique)
    {
        uniqueList = unique.uniqueList;
    }

}
