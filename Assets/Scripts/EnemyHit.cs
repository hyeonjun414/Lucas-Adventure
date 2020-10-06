using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject obj;
    Enemy enemy;
    ItemDrop drop;
    Material mat;
    Rigidbody2D rigid;
    Animator anim;
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        enemy = GetComponentInParent<Enemy>();
        drop = GetComponentInParent<ItemDrop>();
        mat = GetComponent<SpriteRenderer>().material;
        anim = GetComponent<Animator>();
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            enemy.curHealth -= weapon.damage;
            Vector2 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Melee : " + enemy.curHealth);
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            enemy.curHealth -= bullet.damage;
            Vector2 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Range : " + enemy.curHealth);
        }
    }
    IEnumerator OnDamage(Vector2 reactVec)
    {

        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (enemy.curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRun", false);
            mat.color = Color.gray;
            gameObject.layer = 9;
            enemy.isDead = true;
            drop.DropItem();
            Destroy(obj, 2);
        }

    }
}
