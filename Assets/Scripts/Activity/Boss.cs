using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossName
    {
        Area1,
        Area2,
    }
    public BossName bossName;
    public float speed = 5f; // 적 추적속도
    public int maxHealth; // 최대 체력
    public int curHealth; // 현재 체력
    public int damage; // 적 공격력
    public float exp; // 경험치 획득 량
    public Transform target; // 추적할 대상
    public GameObject Bullet; // 원거리 적이 발사할 탄알 오브젝트

    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Material mat;
    Animator anim;
    bool isDamage = false; // 피해를 입고있는가
    public bool isDead = false; // 죽었는가

    public RectTransform BossHpbar;
    public GameObject AttackReader;
    public LineRenderer lr;

    Vector3 moveVelo;
    Coroutine bossroutine;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        //적 생성시 컴포넌트들의 연결을 해줌
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        BossHpbar = GameObject.Find("BossUI").transform.Find("Boss Group").transform.Find("Image").transform.Find("Boss Health Image").gameObject.GetComponent<RectTransform>();
        target = FindObjectOfType<Player>().transform;
        //적의 행동패턴을 시작
        StartCoroutine(Waiting());
    }

    void FixedUpdate()
    {
        anim.SetBool("isRun", rigid.velocity != Vector2.zero);
        HPbarUpdate();
        EnemyDie();
    }
    
    
    
    

    void EnemyDie()
    {
        if (isDead)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.exp += exp;
                exp = 0;
            }
            StopCoroutine(bossroutine);
            DrawAttackDelete();

        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2f);
        if(bossName == BossName.Area1)
            bossroutine = StartCoroutine(BossRoutine());
        else
            bossroutine = StartCoroutine(BossRoutine2());
        
    }
    IEnumerator BossRoutine()
    {
        int routineflag = Random.Range(0, 2);
        
        switch (routineflag)
        {
            case 0: // 탄알 발사
                for(int i=0; i<3; i++)
                {
                    DrawAttack();
                    yield return new WaitForSeconds(1f);
                    Shoting();
                    DrawAttackDelete();

                }
                break;
            case 1: // 대쉬 공격
                for (int i = 0; i < 3; i++)
                {
                    DrawAttack();
                    yield return new WaitForSeconds(0.5f);
                    Dash();
                    yield return new WaitForSeconds(1.5f);
                    rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    DrawAttackDelete();
                }
                break;
        }
        rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3f);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;


        bossroutine = StartCoroutine(BossRoutine());
    }
    IEnumerator BossRoutine2()
    {
        int routineflag = Random.Range(0, 3);
        
        switch (routineflag)
        {
            case 0: // 탄알 발사
                for(int i=0; i<3; i++)
                {
                    DrawAttack();
                    yield return new WaitForSeconds(1f);
                    Shoting();
                    DrawAttackDelete();

                }
                break;
            case 1: // 대쉬 공격
                for (int i = 0; i < 3; i++)
                {
                    DrawAttack();
                    yield return new WaitForSeconds(0.5f);
                    Dash();
                    yield return new WaitForSeconds(1.5f);
                    rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                    rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    DrawAttackDelete();
                }
                break;  
            case 2: // 탄알 발사
                transform.position = Vector2.zero;
                yield return new WaitForSeconds(1f);
                //DrawAttack();
                for(int j = 0; j<360; j+=5){
                    ShotingWithoutTracing(j);
                    yield return new WaitForSeconds(0.1f);
                }
                //DrawAttackDelete();
                break;
        }
        rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(3f);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;


        bossroutine = StartCoroutine(BossRoutine2());
    }
    
    void HPbarUpdate()
    {
        BossHpbar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, curHealth);
    }

    void DrawAttack()
    {
        moveVelo = new Vector3(target.position.x - transform.position.x,
                               target.position.y - transform.position.y, 0).normalized;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, moveVelo*500);
    }
    void DrawAttackDelete()
    {
        moveVelo = Vector3.zero;
        lr.SetPosition(0, moveVelo);
        lr.SetPosition(1, moveVelo);

    }
    void Shoting()
    {
       // 탄알을 생성하고 타겟의 위치에 탄알을 일정한 속도로 발사
       GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
       Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
       rigidBullet.velocity = moveVelo * 2;
    }
    void ShotingWithoutTracing(int angle)
    {
        
        // 탄알을 생성하고 타겟의 위치에 탄알을 일정한 속도로 발사
        GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
        instantBullet.transform.Rotate(0,0,angle);
        Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
        rigidBullet.velocity = instantBullet.transform.up * 2;
    }
    void Dash()
    {
        //방향에 일정한 속도를 더해줌
        //rigid.AddForce(moveVelo * 3000 *Time.deltaTime, ForceMode2D.Impulse);
        rigid.velocity = moveVelo * 3000 * Time.deltaTime;
            // 속력의 방향에 따라 이미지의 좌우 방향을 정해줌
        if (rigid.velocity.x >= 0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (rigid.velocity.x <= -0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
}
