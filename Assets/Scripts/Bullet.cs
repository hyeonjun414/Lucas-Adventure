using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Wall" || 
            (gameObject.name == "EnemyBullet" && other.tag == "Player"))
        {
            Destroy(gameObject);
        }
    }
}
