﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject obj;
    public GameObject dropitem;
    Enemy enemy;
    Material mat;
    Rigidbody2D rigid;
    Animator anim;
    bool isDamege; // 중첩피해를 막기위한 무적시간
    void Start()
    {
        // 컴포넌트의 연결
        rigid = GetComponentInParent<Rigidbody2D>();
        enemy = GetComponentInParent<Enemy>();
        mat = GetComponent<SpriteRenderer>().material;
        anim = GetComponent<Animator>();
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌하는 객체의 태그가 Melee라면 아래의 코드를 실행.
        if (other.tag == "Melee" && !enemy.isDead && !isDamege)
        {
            // 충돌 객체의 무기 정보를 받아옴
            Weapon weapon = other.GetComponent<Weapon>();
            // 무기의 공격력만큼 적의 체력을 감소시킴.
            enemy.curHealth -= weapon.damage;
            // 공격을 당할때 밀려나는 방향을 계산.
            Vector2 reactVec = (transform.position - other.transform.position).normalized;
            // 피격 코루틴 실행.
            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Melee : " + enemy.curHealth);
        }
    }
    // 피격 코루틴
    IEnumerator OnDamage(Vector2 reactVec)
    {
        // 피격 코루틴이 실행되면
        mat.color = Color.red;
        isDamege = true;
        // 넉백을 실행.
        rigid.AddForce(reactVec*10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);

        if (enemy.curHealth > 0)
        {
            // 체력이 남아있다면 다시 원래색으로 돌려놓음.
            mat.color = Color.white;
        }
        else
        {
            // 체력이 0이 되면 움직임을 정지시키고, 회색으로 만든 후에, 잠시후 사라지게 만듬.
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRun", false);
            mat.color = Color.gray;
            gameObject.layer = 9;
            enemy.isDead = true;
            Instantiate(dropitem, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            
            Destroy(enemy.gameObject, 2);
        }
        yield return new WaitForSeconds(0.1f);
        isDamege = false;

    }
}
