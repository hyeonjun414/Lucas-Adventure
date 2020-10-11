using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {Ammo, Coin, Weapon, Heart};
    public Type type;
    public int value; //양
    // Start is called before the first frame update
    void Start()
    {
        if (type == Type.Coin)
            CoinGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CoinGenerate()
    {
        float rand_x, rand_y;
        rand_x = Random.Range(-1f, 1f);
        rand_y = Random.Range(-1f, 1f);
        gameObject.transform.position = new Vector3(transform.position.x + rand_x,
                                        transform.position.y + rand_y, 0);
    }
}
