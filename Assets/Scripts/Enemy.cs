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
    public Transform target;
    public GameObject Bullet;
    int movementFlag = 0;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Material mat;
    Animator anim;

    bool isTracing = false;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        mat = GetComponentInChildren<SpriteRenderer>().material;
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        StartCoroutine("ChangeMovement");
        if(type == Type.Range)
            InvokeRepeating("Shoting", 0, 1);
    }
    
    void FixedUpdate()
    {
        Move();
        Tracing();
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
        if (isTracing && type == Type.Range)
        {
            GameObject instantBullet = Instantiate(Bullet, transform.position, transform.rotation);
            Rigidbody2D rigidBullet = instantBullet.GetComponent<Rigidbody2D>();
            rigidBullet.velocity = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0).normalized * 3;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector2 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Melee : " + curHealth);
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector2 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
            Debug.Log("Range : " + curHealth);
        }
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

        if (movementFlag == 0)
        {

        }
        else
        { 
        }
        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }
    IEnumerator OnDamage(Vector2 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 9;

            reactVec = reactVec.normalized;
            reactVec += Vector2.up;
            rigid.AddForce(reactVec, ForceMode2D.Impulse);

            Destroy(gameObject, 2);
        }

    }

}
