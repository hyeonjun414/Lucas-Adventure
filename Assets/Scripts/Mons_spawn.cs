using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mons_spawn : MonoBehaviour
{
    public int maxCount;
    public int enemyCount;
    public float spwanTime = 3f;
    public float curTime;
    public bool[] isSpawn; //중복위치금지

    public Transform[] spawnPoints;
    public GameObject Enemy1;   //근접
    //public GameObject Enemy2;   //원거리

    public static Mons_spawn _instance;

    private void Start()
    {
        isSpawn = new bool[spawnPoints.Length]; //isSpawn배열의 크기를 spawnPoints의 크기만큼 초기화
        for (int i = 0; i<isSpawn.Length; i++)
        {
            isSpawn[i] = false;
        }
        _instance = this;
    }

    private void Update()
    {
        if (curTime >= spwanTime && enemyCount< maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if(!isSpawn[x])
            SpawnEnemy(x);
        }
        curTime += Time.deltaTime;
        
    }

    public void SpawnEnemy(int ranNum)
    {
        curTime = 0;
        enemyCount++;
        Instantiate(Enemy1, spawnPoints[ranNum]);
        
        isSpawn[ranNum] = true; //랜덤 값이 true => SpawnEnemy 실행x

    }

}
