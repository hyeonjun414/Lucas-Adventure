﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActive : MonoBehaviour
{
    public static ItemActive _instance;
    [SerializeField] private Reward_set reward;
    // Start is called before the first frame update
    

    private void Update()
    {

        if (Gate_set._instance.EnemyCnt <= 0) //EnemyCnt값이 -495이하이면 게이트가 열린다.
        {
            reward.ItemVisible();
        }


    }
}