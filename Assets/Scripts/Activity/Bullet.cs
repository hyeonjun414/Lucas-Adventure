using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" || 
            (gameObject.name == "EnemyBullet(Clone)" && other.tag == "Player"))
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
