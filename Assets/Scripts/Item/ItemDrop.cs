using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] Weapon;
    public GameObject Coin;
    int n;
    public void DropItem()
    {
        n = Random.Range(0, 2);
        if(n == 0)
        {
            n = Random.Range(0, 10);
            for(int i=0; i<n; i++)
            {
                GameObject instantItem = Instantiate(Coin,
                                 new Vector2(transform.position.x, transform.position.y),
                                 Quaternion.identity);
                Destroy(instantItem, 15f);
            }
            
        }
        
        else if (n == 1)
        {
            n = Random.Range(0, 3);
            GameObject instantItem = Instantiate(Weapon[n],
                                 new Vector2(transform.position.x, transform.position.y),
                                 Weapon[n].transform.rotation);
            Destroy(instantItem, 15f);
        }
        
    }
}
