using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum Type { Melee, Range,  BMonster}; // 적 타입
    public Type type; // 적 타입에 접근할 변수

    public float speed; // 적 추적속도
    public int maxHealth; // 최대 체력
    public int curHealth; // 현재 체력
    public int damage; // 적 공격력
    public float exp; // 경험치 획득 량
    public Transform target; // 추적할 대상
    public GameObject Bullet; // 원거리 적이 발사할 탄알 오브젝트
    int movementFlag = 0; // 적의 자율 행동패턴 변수

    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Material mat;
    Animator anim;
    bool isTracing = false; // 추적중인가
    bool isDamage = false; // 피해를 입고있는가
    public bool isDead = false; // 죽었는가

  

    void Awake()
    {
        //적 생성시 컴포넌트들의 연결을 해줌
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        //적의 행동패턴을 시작
        StartCoroutine("ChangeMovement");
        if(type == Type.Range) //원거리 적은 1초마다 탄알을 발사
            InvokeRepeating("Shoting", 0, 1);
    }
    
    void FixedUpdate()
    {
        if (!isDead) //죽었을 경우 추적과 이동을 중지
        {
            Move();
            Tracing();
        }
        EnemyDie();
    }
    void Move()
    {
        if (isDamage) return; //공격당하는 중에는 경직, 이동불가
        
        //추적중이거나 원거리 적일 경우 이동 안함
        if (isTracing || type == Type.Range) return;

        //속력을 계산할 변수 생성
        Vector3 moveVelocity = new Vector3();

        if(movementFlag == 1) // 왼쪽으로 이동
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movementFlag == 2) // 오른쪽으로 이동
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // 설정된 방향으로 일정한 속도로 이동함
        rigid.velocity = moveVelocity * 100f *Time.deltaTime;

        // 만약 속력이 있다면 애니메이션 실행
        anim.SetBool("isRun", rigid.velocity != Vector2.zero);
    }

    void Tracing()
    {
        //만약 추적중이고 근거리 적이라면 실행
        if (isTracing && (type == Type.Melee || type == Type.BMonster))
        {
            // 타겟의 위치에서 자신의 위치를 빼서 방향을 계산함
            Vector3 movevelo = new Vector3(target.position.x - transform.position.x, 
                                            target.position.y - transform.position.y, 0).normalized;

            //방향에 일정한 속도를 더해줌
            rigid.AddForce(movevelo * speed * Time.deltaTime, ForceMode2D.Impulse);

            // 속력의 방향에 따라 이미지의 좌우 방향을 정해줌
            if (rigid.velocity.x >= 0.01f)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (rigid.velocity.x <= -0.01f)
                transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        //속력이 있다면 애니메이션 실행
        anim.SetBool("isRun", rigid.velocity != Vector2.zero);
    }

    void Shoting()
    {
        // 추적중이고 원거리 적이라면 실행
        if (isTracing && type == Type.Range && !isDead)
        {
            // 탄알을 생성하고 타겟의 위치에 탄알을 일정한 속도로 발사
            GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
            rigidBullet.velocity = new Vector3(target.position.x - transform.position.x, 
                                               target.position.y - transform.position.y, 0).normalized * 5;
        }
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
                
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 콜라이더의 반경에 플레이어가 들어오면 추적을 시작하고 위치를 받아옴
        if (other.tag == "Player")
        {
            target = other.transform;
            StopCoroutine("ChangeMovement");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // 만약 콜라이더의 반경안에 플레이어가 들어와있다면 위치를 지속적으로 갱신
        if (other.tag == "Player")
        {
            isTracing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 만약 플레이어가 콜라이더의 바깥으로 나가면 추적을 취소하고 정지함
        if (other.tag == "Player")
        {
            isTracing = false;
            rigid.velocity = Vector2.zero;
            StartCoroutine("ChangeMovement");
        }
    }
    IEnumerator ChangeMovement()
    {
        // 행동패턴을 랜덤으로 받아 실행함
        movementFlag = Random.Range(0, 3);
        
        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }
    void BossRoutine()
    {

    }
}
