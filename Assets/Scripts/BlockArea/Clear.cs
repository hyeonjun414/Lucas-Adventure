using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{

    public int Cnt;//보스가 죽으면 카운트
    public static Clear _instance; //BossHit에서 Clear에 접근하기 위한 정적변수
    [SerializeField] private Block active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cnt > 0) 
        {
            active.BlockArea();//블록활성화
        }
        
    }

    public void B_count()
    {
        Cnt++;
    }


}
