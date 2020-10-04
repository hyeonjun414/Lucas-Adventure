using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public GameObject Bullet;
    bool isPlayer;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoting", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        
    }
    void Shoting()
    {
        if (isPlayer)
        {
            GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
            rigidBullet.velocity = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y).normalized * 3;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pos = collision.gameObject.transform.position;
            isPlayer = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayer = false;
        }

    }
}
