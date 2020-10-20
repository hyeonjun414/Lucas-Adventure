using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    Player player; // 부모 오브젝트에 있는 플레이어 객체
    SpriteRenderer sprite; // 이미지 조작에 사용되는 컴포넌트
    Rigidbody2D rigid; // 물리효과에 사용되는 컴포넌트

    void Start()
    {
        //컴포넌트 연결
        rigid = GetComponentInParent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
        sprite = GetComponent<SpriteRenderer>();
        
    }
    void OnCollisionStay2D(Collision2D other)
    {
        // 충돌 객체가 적이라면 아래 코드를 실행
        if (other.gameObject.tag == "Enemy")
        {
            // 플레이어가 피격중이라면 무효처리
            if (!player.isDamage)
            {
                // 충돌객체의 정보에서 공격력을 가져와 피격을 계산
                Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
                player.health -= enemy.damage;
                // 넉백 방향 계산
                Vector2 vec = (player.transform.position - enemy.transform.position);
                // 피격 코루틴 실행
                StartCoroutine(OnDamage(vec));
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌 객체가 적 탄알이라면 아래 코드를 실행
        if (other.tag == "EnemyBullet")
        {
            // 플레이어 피격중에는 무효
            if (!player.isDamage)
            {
                // 충돌객체의 정보에서 공격력을 가져와 피격을 계산
                Bullet enemyBullet = other.GetComponent<Bullet>();
                player.health -= enemyBullet.damage;
                // 넉백 방향 계산
                Vector2 vec = (player.transform.position - enemyBullet.transform.position);
                // 피격 코루틴 실행, 하지만 근접보다는 조금 약하게
                StartCoroutine(OnDamage(vec/2));
            }
        }
    }
    // 피격 코루틴
    IEnumerator OnDamage(Vector2 vec)
    {
        Debug.Log("hit");
        // 플레이어가 피격중임을 표시
        player.isDamage = true;
        // 플레이어의 피격중 색을 변경.
        sprite.material.color = Color.yellow;
        // 넉백 물리력 계산
        rigid.AddForce(vec * 1000f * Time.deltaTime, ForceMode2D.Impulse);

        //피해를 무시하는 무적시간을 부여
        yield return new WaitForSeconds(0.5f);
        
        //무적시간이 끝나면 피격이 끝났음을 알리고, 색을 원상복구 시킴
        player.isDamage = false;
        sprite.material.color = Color.white;
    }
    
}
