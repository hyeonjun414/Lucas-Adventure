using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 5f; //기본 이동속도

    public int coin;  //가지고 있는 코인
    public int health;// 현재 체력
    public float exp;   // 현재 경험치
    public int level; // 현재 레벨

    public int maxcoin; //최대 소지가능 코인
    public int maxhealth; // 최대 체력
    public float maxExp; // 레벨업에 필요한 경험치
    public float maxSpeed = 10f; //이동속도 제한 : 물리력을 더하는 방식이라 필요

    float hAxis, vAxis; //상하좌우 이동 입력
    bool iDown; // 인벤토리창 입력
    bool fDown;  // 공격

    public bool InputAttack = false;

    bool isMotion = false; //현재 행동중인지
    bool isFireReady; //현재 공격할 준비가 되었는지
    public bool isDamage; //현재 공격 당하고 있는지
    bool isAttack = false; // 현재 공격중인지
    public bool isDead = false;
    bool UIon = false; // 현재 UI가 켜져있는지
    
    float fireDelay = 0; //공격 딜레이

    //필요 컴포넌트
    public ParticleSystem dust;
    public ParticleSystem dash;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;
    Animator anim;
    GameObject nearObject;
    public Weapon equipWeapon;
    public Inventory inven;
    public GameObject ArmL;
    public JoystickValue value;
    
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        UiCheck();
        GetInput();
        Attack();
        levelup();
        Swap();
        // 입력 값에 따라 좌우 이미지 좌우 반전
        if (Input.GetButton("Horizontal"))
            if(Input.GetAxisRaw("Horizontal") > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if(Input.GetAxisRaw("Horizontal") < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        if (value.joyTouch.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (value.joyTouch.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

    }
    void FixedUpdate()
    {
        Move();
    }

    //입력 키 모음
    void GetInput()
    {
        if(value.joyTouch == Vector2.zero)
        {
            hAxis = Input.GetAxisRaw("Horizontal");
            vAxis = Input.GetAxisRaw("Vertical");
        }
        else
        {
            hAxis = value.joyTouch.x;
            vAxis = value.joyTouch.y;
        }
        
        fDown = Input.GetButtonDown("Fire1");
        iDown = Input.GetButtonDown("Interaction");
    }
    //공격
    void Attack()
    {
        if(UIon == true) return;
        if(equipWeapon == null) return; //현재 장착 무기가 없다면 취소

        //공격딜레이시간에 프레임단위로 시간을 측정하고,
        fireDelay += Time.deltaTime;
        //공격딜레이시간이 장착한 무기의 딜레이보다 커지면 공격 준비
        isFireReady = equipWeapon.rate < fireDelay;

        //공격키가 눌리고 공격준비가 완료되면 공격
        if((fDown && isFireReady) || (InputAttack && isFireReady))
        {
            StartCoroutine("icantswap"); //공격중에는 교체 불가
            anim.SetTrigger("isAttack"); //공격 애니메이션의 실행
            equipWeapon.Use(); //장착무기의 공격루틴 활성화
            
            fireDelay = 0; //플레이어 공격딜레이를 다시 초기화
            InputAttack = false;
        }
    }
    //이동
    void Move()
    {
        if (isDamage || isAttack) return; //공격당하는 중에는 경직, 이동불가
        Vector2 moveVelocity = new Vector2(hAxis, vAxis); //입력값에 따른 속력의 방향 대입
        anim.SetBool("isRun", moveVelocity != Vector2.zero); //멈춰있는게 아니라면 달리기 애니메이션 실행
        rigid.AddForce(moveVelocity * Speed * Time.deltaTime, ForceMode2D.Impulse); //플레이어에 프레임 단위로 속력을 더해줌

        //일정속도 이상으로 올라가지 않도록 속도 제한
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

        if (moveVelocity != Vector2.zero) CreateDust(); //달리기 효과 생성
        
    }
    //교체
    void Swap()
    {
        if (isMotion) return; //움직이는 중에는 취소

        //현재 자신이 장착중인 슬롯으로 교체하려고 하면 취소
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        
        int weaponIndex = -1;
        //입력된 키에따라 무기순서 부여
        if (sDown1) weaponIndex = 0;
        else if (sDown2) weaponIndex = 1;
        if (weaponIndex < 0) return;
        
        GameObject go = ArmL.transform.Find(weapons[weaponIndex].itemName+"(Clone)").gameObject;
        //교체 1번이나 교체 2번이 눌리면 실행
        if ((sDown1 || sDown2) && go != null)
        {
            if(equipWeapon != null) // 현재 장착중인 무기가 있다면 비활성화
                equipWeapon.gameObject.SetActive(false);
            equipWeaponIndex = weaponIndex; //현재 장착한 무기 순서 업데이트
            equipWeapon = go.GetComponent<Weapon>(); //현재무기를 슬롯의 무기를 불러와 장착
            equipWeapon.gameObject.SetActive(true); //바꿀 무기 활성화
            
        }
    }

    //교체가능
    IEnumerator icantswap()
    {
        isMotion = true;
        yield return new WaitForSeconds(equipWeapon.rate);
        isMotion = false;
    }
    //달리기 효과
    void CreateDust()
    {
        dust.Play();
    }

    void levelup()
    {
        if (exp >= maxExp)
        {
            level++;
            exp = 0;
            maxExp *= 1.1f;
            maxExp = Mathf.Round(maxExp);
        }
            
        
    }
    public void WeaponEquip(int i)
    {
        Transform ArmL = gameObject.transform.Find("ArmL");
      //  GameObject go = Instantiate(weapons[i], 
    }

    public void UiCheck()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("UIpanel");
        for(int i = 0; i<go.Length; i++)
        {
            if (go[i].activeSelf == true)
            {
                UIon = true;
                return;
            }
        }
        UIon = false;
        return;
    }
    
}
