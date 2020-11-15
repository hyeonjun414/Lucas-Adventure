using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private static bool playerExists;

    static public Player instance;  //선언된 변수의 값을 공유
   

    public float Speed = 5f; //기본 이동속도

    public int damage = 10;
    public int armor = 0;


    public int coin;  //가지고 있는 코인
    public int health;// 현재 체력
    public float exp;   // 현재 경험치
    public int level; // 현재 레벨

    public string curMapName;

    public int maxcoin; //최대 소지가능 코인
    public int maxhealth; // 최대 체력
    public float maxExp; // 레벨업에 필요한 경험치
    public float maxSpeed = 10f; //이동속도 제한 : 물리력을 더하는 방식이라 필요

    float hAxis, vAxis; //상하좌우 이동 입력
    bool iDown; // 인벤토리창 입력
    bool fDown;  // 공격

    public bool InputAttack = false;
    public bool InputSwap = false;
    public GameObject InteractionBtn;

    bool isMotion = false; //현재 행동중인지
    bool isFireReady; //현재 공격할 준비가 되었는지
    public bool isDamage; //현재 공격 당하고 있는지
    bool isAttack = false; // 현재 공격중인지
    public bool isDead = false;
    bool UIon = false; // 현재 UI가 켜져있는지
    
    float fireDelay = 0; //공격 딜레이

    //필요 컴포넌트
    public ParticleSystem dust;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Vector3 movement;
    Animator anim;
    public Item equipWeapon; // 현재 장착중인 무기
    public Weapon equipWeaponto;
    public Inventory inven;
    public GameObject ArmL;
    public JoystickValue value;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
            SelfDestroy();
            curWeaponCheck();
        UiCheck();
        GetInput();
        Attack();
        levelup();
        Swap();

        // 입력 값에 따라 좌우 이미지 좌우 반전
        if (!isAttack)
        {
            if (Input.GetButton("Horizontal"))
                if (Input.GetAxisRaw("Horizontal") > 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else if (Input.GetAxisRaw("Horizontal") < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            if (value.joyTouch.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (value.joyTouch.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        

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
        if(equipWeaponto == null) return; //현재 장착 무기가 없다면 취소

        //공격딜레이시간에 프레임단위로 시간을 측정하고,
        fireDelay += Time.deltaTime;
        //공격딜레이시간이 장착한 무기의 딜레이보다 커지면 공격 준비
        isFireReady = equipWeaponto.rate < fireDelay;

        //공격키가 눌리고 공격준비가 완료되면 공격
        if((fDown && isFireReady) || (InputAttack && isFireReady) && !isAttack)
        {
            StartCoroutine("Icantswap"); //공격중에는 교체 불가
            anim.SetTrigger("isAttack"); //공격 애니메이션의 실행
            equipWeaponto.Use(inven.equipWeapon[0]); //장착무기의 공격루틴 활성화
            fireDelay = 0; //플레이어 공격딜레이를 다시 초기화
            
        }
    }
    //이동
    void Move()
    {
        if (isDamage) return; //공격당하는 중에는 경직, 이동불가
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
        //만약 현재 무기의 사용횟수가 다 소비되면 파괴
        if(inven.equipWeapon[0].itemCount == 0)
        {
            inven.equipWeapon.RemoveAt(0);
        }

        //현재 공격중이아니고 스왑버튼이 눌리면 교체 실행
        if (InputSwap == true && !isMotion)
        {
            Item curWeapon = inven.equipWeapon[0];
            Debug.Log(curWeapon.itemName);
            inven.equipWeapon[0] = null;
            for (int i = 1; i < inven.equipWeapon.Count; i++)
            {
                if (inven.equipWeapon[i-1] == null)
                {
                    inven.equipWeapon[i - 1] = inven.equipWeapon[i];
                    inven.equipWeapon[i] = null;
                }
            }
            inven.equipWeapon[inven.equipWeapon.Count - 1] = curWeapon;
            InputSwap = false;
        }
    }
    //교체가능
    IEnumerator Icantswap()
    {
        isMotion = true;
        isAttack = true;
        yield return new WaitForSeconds(equipWeaponto.rate);
        isMotion = false;
        isAttack = false;
        InputAttack = false; // 공격이 끝나야 공격입력을 받음

    }
    //달리기 효과
    void CreateDust()
    {
        dust.Play();
    }
    // 레벨 상승
    void levelup()
    {
        if (exp >= maxExp)
        {
            level++;
            exp = exp-maxExp;
            maxExp *= 1.1f;
            maxExp = Mathf.Round(maxExp);
            damage += 5;
            armor += 2;
            equipWeaponto.damage += 5;
        }
    }
    // 인터페이스가 활성화되었는지 확인
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
    // 현재 무기가 어떤건지 확인
    public void curWeaponCheck()
    {
        Item curWeapon = inven.equipWeapon[0];
        Transform go = ArmL.transform.Find(equipWeapon.itemName);
        //초기 장비 지급
        if (equipWeapon.itemName == "")
        {
            equipWeapon = curWeapon;
            GameObject weapon = (GameObject)Instantiate(Resources.Load("Weapon/" + equipWeapon.itemName),
                new Vector3(ArmL.transform.position.x, ArmL.transform.position.y,0), ArmL.transform.rotation);
            weapon.transform.parent = ArmL.transform;
            weapon.transform.localScale = new Vector3(1,1,1);
            Weapon weaponto = weapon.GetComponent<Weapon>();
            equipWeaponto = weaponto;
            equipWeaponto.damage += damage;
            return;
        }
        //장착중인 무기와 현재 장비가 다르면
        if(equipWeapon.itemName != curWeapon.itemName)
        {
            GameObject equipgo = GameObject.Find(equipWeapon.itemName+"(Clone)");
            Destroy(equipgo);
            equipWeapon = curWeapon;
            GameObject weapon = (GameObject)Instantiate(Resources.Load("Weapon/" + equipWeapon.itemName),
                new Vector3(ArmL.transform.position.x, ArmL.transform.position.y, 0), ArmL.transform.rotation);
            weapon.transform.parent = ArmL.transform;
            weapon.transform.localScale = new Vector3(1, 1, 1);
            Weapon weaponto = weapon.GetComponent<Weapon>();
            equipWeaponto = weaponto;
            equipWeaponto.damage += damage;
        }
        switch (equipWeaponto.type)
        {
            case Weapon.Type.ShotSword:
                anim.SetBool("EquipShotSword", true);
                anim.SetBool("EquipSword", false);
                anim.SetBool("EquipHeavyWeapon", false);
                anim.SetBool("EquipSpear", false);
                anim.SetBool("EquipMace", false);
                break;
            case Weapon.Type.Sword:
                anim.SetBool("EquipShotSword", false);
                anim.SetBool("EquipSword", true);
                anim.SetBool("EquipHeavyWeapon", false);
                anim.SetBool("EquipSpear", false);
                anim.SetBool("EquipMace", false);
                break;
            case Weapon.Type.HeavyWeapon:
                anim.SetBool("EquipShotSword", false);
                anim.SetBool("EquipSword", false);
                anim.SetBool("EquipHeavyWeapon", true);
                anim.SetBool("EquipSpear", false);
                anim.SetBool("EquipMace", false);
                break;
            case Weapon.Type.Spear:
                anim.SetBool("EquipShotSword", false);
                anim.SetBool("EquipSword", false);
                anim.SetBool("EquipHeavyWeapon", false);
                anim.SetBool("EquipSpear", true);
                anim.SetBool("EquipMace", false);
                break;
            case Weapon.Type.Mace:
                anim.SetBool("EquipShotSword", false);
                anim.SetBool("EquipSword", false);
                anim.SetBool("EquipHeavyWeapon", false);
                anim.SetBool("EquipSpear", false);
                anim.SetBool("EquipMace", true);
                break;
            default: break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Store"))
        {
            InteractionBtn.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Store"))
        {
            InteractionBtn.SetActive(false);
        }
    }
    void SelfDestroy()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            playerExists = false;
            Destroy(gameObject);
        }
    }
}

