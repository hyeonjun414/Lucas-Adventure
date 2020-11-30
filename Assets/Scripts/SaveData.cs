using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class SaveData {

    public float Speed; //기본 이동속도

    public int damage;
    public int armor;

    public string curMapName;
    public int coin;  //가지고 있는 코인
    public int health;// 현재 체력
    public float exp;   // 현재 경험치
    public int level;

    public float x;
    public float y;

    public SaveData(Player player)
    {
        Speed = player.Speed;
        health = player.health; //hp
        level = player.level; //level
        damage = player.damage; // damage
        armor = player.armor; //아머
        coin = player.coin; //코인
        exp = player.exp; //경험치
        curMapName = player.curMapName; //맵
      
        x = player.transform.position.x;  //x좌표
        y = player.transform.position.y;  //y좌표
    }


}
