using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBtn : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public void OnClickSwap()
    {
        if (player.isWeapon == 0)
            player.isWeapon = 1;
        else if (player.isWeapon == 1)
            player.isWeapon = 0;
    }
    
    public void OnClickAttack()
    {
        player.InputAttack = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
