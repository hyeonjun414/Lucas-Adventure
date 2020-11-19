using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_set : MonoBehaviour
{
    public static Reward_set _instance;
    private bool state;
    public GameObject Target;


        public void Start()
    {
        gameObject.SetActive(false);
    }

    public void ItemVisible()
    {
        gameObject.SetActive(true);
    }



}

