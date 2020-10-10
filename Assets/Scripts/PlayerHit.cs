using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    Player player;
    SpriteRenderer sprite;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
        sprite = GetComponent<SpriteRenderer>();
        
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!player.isDamage)
            {
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
                player.health -= enemy.damage;
                Vector2 vec = (player.transform.position - enemy.transform.position);
                StartCoroutine(OnDamage(vec));
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            if (!player.isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                player.health -= enemyBullet.damage;
                Vector2 vec = (player.transform.position - enemyBullet.transform.position);
                StartCoroutine(OnDamage(vec/2));
            }
        }
    }
    IEnumerator OnDamage(Vector2 vec)
    {
        Debug.Log("hit");
        player.isDamage = true;
        sprite.material.color = Color.yellow;
        rigid.AddForce(vec * 1000f * Time.deltaTime, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        
        player.isDamage = false;
        sprite.material.color = Color.white;
    }
    
}
