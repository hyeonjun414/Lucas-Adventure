using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_set : MonoBehaviour
{
    [SerializeField] private Gate_Open gate;

    public Transform[] spawnpoints; //몬스터 위치
    public GameObject enemy;    //몬스터 객체
    public GameObject enemy_range;

    public int Max;//몬스터 최대 생성 가능한 수
    public int EnemyCnt=0; //몬스터 카운트
    public int EnemyCnt_range;
    public bool[] isSpawn;

    public static Gate_set _instance; //Enemy에서 Gate_set에 접근하기 위한 정적변수


    private void Start()
    {
        isSpawn = new bool[spawnpoints.Length];
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            isSpawn[i] = false;
        }
        _instance = this;

        for(;EnemyCnt<Max;)
        {
            int x = Random.Range(0, spawnpoints.Length); //0~max수까지
            if (!isSpawn[x]) //해당위치가 false이면 그 위치에 생성
                SpawnEnemy(x); // 몬스터 생성
        }
                /*
        if (EnemyCnt < Max) //Max보다 작으면 생성
        {
            int x = Random.Range(0, spawnpoints.Length); //0~max수까지
            if (!isSpawn[x]) //해당위치가 false이면 그 위치에 생성
                SpawnEnemy(x); // 몬스터 생성
        }*/

    }


    // Update is called once per frame
    private void Update()
    {
        
        if (EnemyCnt<=0) //EnemyCnt값이 -495이하이면 게이트가 열린다.
        {
            gate.OpenGate();
        }

       
    }

    public void SpawnEnemy(int randNum) //몬스터 생성함수
    {
        EnemyCnt++; //생성될때마다 몬스터 수 카운트
        if (EnemyCnt_range != 0)
        { 
            Instantiate(enemy_range, spawnpoints[randNum]);
            EnemyCnt_range--;
        }
        else
            Instantiate(enemy, spawnpoints[randNum]);   //몬스터소환, 랜덤위치 생성
        isSpawn[randNum] = true; //해당위치가 true이면 그 위치에 자동생성 x
    }
    public void countdown()
    {
        EnemyCnt--;
    }

}
