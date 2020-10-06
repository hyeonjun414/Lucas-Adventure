using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Info : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemDrop()
    {
        int rand_num = Random.Range(0, 4);
        if( rand_num >= 0 && rand_num < 4)
        {
            GameObject DropNode = null;
            DropNode = (GameObject)Instantiate(Resources.Load("Prefabs/Items"));
            DropNode.transform.position = this.transform.position;
            
        }
    }
}
