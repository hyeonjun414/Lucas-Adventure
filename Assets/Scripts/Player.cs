using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public int coin;
    public int health;

    public int maxcoin;
    public int maxhealth;
    public float maxSpeed = 10f;
    float hAxis, vAxis;

    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool fDown;
    bool zDown;

    bool isMotion;
    bool isFireReady;
    bool isBorder;
    public bool isDamage;

    public ParticleSystem dust;
    public ParticleSystem dash;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;

    Animator anim;
    GameObject nearObject;
    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    float fireDelay;


    void Start()
    {
        isMotion = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        GetInput();
        Attack();
        
        Swap();
        Interaction();
        if (Input.GetButton("Horizontal"))
            if(Input.GetAxisRaw("Horizontal") > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if(Input.GetAxisRaw("Horizontal") < 0)
                transform.localScale = new Vector3(-1, 1, 1);
                
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        fDown = Input.GetButtonDown("Fire1");
        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        zDown = Input.GetButtonDown("Dash");
    }
    void FixedUpdate()
    {
        Move();
        Dash();
    }
    
    //공격
    void Attack()
    {
        if(equipWeapon == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady)
        {
            StartCoroutine("icanswap");
            anim.SetTrigger("isAttack");
            equipWeapon.Use();
            
            fireDelay = 0;
        }
    }
    //이동
    void Move()
    {
        if (isDamage) return;
        Vector2 moveVelocity = new Vector2(hAxis, vAxis);
        anim.SetBool("isRun", moveVelocity != Vector2.zero);
        rigid.AddForce(moveVelocity * Speed * Time.deltaTime, ForceMode2D.Impulse);

        if(hAxis > 0) //x축 이동 계산
        {
            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
        }
        else if(hAxis < 0)
        {
            if (rigid.velocity.x < maxSpeed * -1)
            {
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
            }
        }
        if(vAxis > 0) //y축 이동 계산
        {
            if (rigid.velocity.y > maxSpeed)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed);
            }
        }
        else if(vAxis < 0)
        {
            if (rigid.velocity.y < maxSpeed * -1)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed * -1);
            }
        }
        if (moveVelocity != Vector2.zero) CreateDust(); //먼지 효과
        
    }
    void Dash()
    {
        if (zDown)
        {
            Vector2 moveVelocity = new Vector2(hAxis, vAxis);
            dash.Play();
            rigid.AddForce(moveVelocity * 1000f * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
    void Swap()
    {
        if (isMotion)
            return;
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3))
        {
            if(equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            
        }
    }

    void Interaction()
    {
        if(iDown && nearObject != null)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxcoin)
                        coin = maxcoin;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxhealth)
                        health = maxhealth;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
            Debug.Log(nearObject.name);
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
    IEnumerator icanswap()
    {
        isMotion = true;
        yield return new WaitForSeconds(equipWeapon.rate);
        isMotion = false;
    }

    void CreateDust()
    {
        dust.Play();
    }
    void CreateDashEffect()
    {
        dash.Play();
    }
}
