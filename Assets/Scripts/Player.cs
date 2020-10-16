using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public string currentMapName;
    public float maxSpeed = 10f;
    float hAxis, vAxis;
    


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;
    Animator anim;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentMapName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneManager.GetActiveScene().name);
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        GetInput();
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
    }
    void FixedUpdate()
    {
        Move();
    }
    
    //이동
    void Move()
    {
        Vector2 moveVelocity = new Vector2(hAxis, vAxis);
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
        
    }
}
