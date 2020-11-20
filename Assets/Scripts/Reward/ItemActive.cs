using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActive : MonoBehaviour
{
    public static ItemActive _instance;
    [SerializeField] private Reward_set reward;
    // Start is called before the first frame update
    

    private void Update()
    {

        if (Gate_set._instance.EnemyCnt <= 0) 
        {
            reward.ItemVisible();
        }


    }
}
