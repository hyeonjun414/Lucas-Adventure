using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static Block _instance;
    private void Start()
    {
        gameObject.SetActive(false); //시작할때 블록비활성화
    }

    public void BlockArea()
    {
        gameObject.SetActive(true);//블록활성화
    }
    
}
