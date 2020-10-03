﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 1000f;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public int coin;
    public int health;

    public int maxcoin;
    public int maxhealth;
    
    float hAxis, vAxis;

    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool fDown;


    bool isFireReady;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;
    

    GameObject nearObject;
    Weapon equipWeapon;
    int equipWeaponIndex = -1;
    float fireDelay;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        GetInput();
        Attack();
        Move();
        Swap();
        
        
        Interaction();
        if (Input.GetButton("Horizontal"))
            if(Input.GetAxisRaw("Horizontal") > 0) transform.localScale = new Vector3(1,1,1);
            else if(Input.GetAxisRaw("Horizontal") < 0) transform.localScale = new Vector3(-1,1,1);
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
    }
    void FixedUpdate()
    {
        
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
            equipWeapon.Use();
            fireDelay = 0;
        }
    }
    //이동
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        
        moveVelocity = new Vector3(hAxis, vAxis, 0);

        rigid.velocity = moveVelocity * Speed * Time.deltaTime;
        //transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Swap()
    {
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
}