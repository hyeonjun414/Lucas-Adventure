﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{

    public string startPoint; //맵의 이동, 플레이어가 시작될 위치
    public Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraPosition();
    }
    
    void SetCameraPosition()
    {
        thePlayer = FindObjectOfType<Player>();

        if (startPoint == thePlayer.curMapName)
        {
            thePlayer.transform.position = gameObject.transform.position;
            //카메라위치
        }
    }

}
