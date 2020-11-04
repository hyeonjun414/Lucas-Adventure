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
        if(!player.InputSwap)
            player.InputSwap = true;
    }
    
    public void OnClickAttack()
    {
        if(!player.InputAttack)
            player.InputAttack = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
