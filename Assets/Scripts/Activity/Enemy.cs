using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;

    public float speed = 200f;
    public int maxHealth;
    public int curHealth;
    public int damage;
    public Transform target;
    public GameObject Bullet;
    public Collider2D coll;
    int movementFlag = 0;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Material mat;
    Animator anim;
    ItemDrop drop;

    bool isTracing = false;
    public bool isDead = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
        anim = GetComponentInChildren<Animator>();
        drop = GetComponent<ItemDrop>();
    }
    void Start()
    {
        StartCoroutine("ChangeMovement");
        if(type == Type.Range)
            InvokeRepeating("Shoting", 0, 1);
    }
    
    void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
            Tracing();
        }
    }
    void Move()
    {
        if (isTracing || type == Type.Range) return;

        Vector3 moveVelocity = Vector3.zero;

        if(movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rigid.velocity = moveVelocity * 100f *Time.deltaTime;
        anim.SetBool("isRun", rigid.velocity != Vector2.zero);
    }

    void Tracing()
    {
        if (isTracing && type == Type.Melee)
        {
            Vector3 movevelo = new Vector3(target.position.x - transform.position.x, 
                                            target.position.y - transform.position.y, 0).normalized;

            
            rigid.velocity = movevelo * speed * Time.deltaTime;

            if (rigid.velocity.x >= 0.01f)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else if (rigid.velocity.x <= -0.01f)
                transform.localScale = new Vector3(1f, 1f, 1f);
        }
        anim.SetBool("isRun", rigid.velocity != Vector2.zero);
    }

    void Shoting()
    {
        if (isTracing && type == Type.Range && !isDead)
        {
            GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
            rigidBullet.velocity = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0).normalized * 5;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTracing = true;
            target = other.transform;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTracing = true;
            target = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTracing = false;
            target = null;
            rigid.velocity = Vector2.zero;
        }
    }
    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);
        
        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }
}
