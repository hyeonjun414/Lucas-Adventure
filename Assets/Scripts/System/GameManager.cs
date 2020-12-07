using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private static bool GMExists; // 게임매니저가 이미 생성되었는지 확인
    public Player player;
    public Boss boss;

    //스테이지 관리
    public string stage;
    public int curArea;

    // UI 연동
    public GameObject Event;
    public GameObject UI;
    public GameObject ItemDB;
    public GameObject statusPanel;
    public GameObject inventoryPanel;
    public GameObject ManuPanel;
    public GameObject BossPanel;

    public Text stageTxt;
    public Text playerLevelTxt;
    public Text playerHealthTxt;
    public Text playerCoinTxt;
    public Text playerExpTxt;

    public Text statusHealthTxt;
    public Text statusDamageTxt;
    public Text statusArmorTxt;
    public Text statusSpeedTxt;

    public Image[] quickSlot;
    public Text[] weaponCntTxt;
    public Image[] invenQuickSlot;
    public Image[] uniqueSlot;
    // UI 연동 끝
    
    Inventory inven;
    Unique unique;
    // 게임매니저 하위 계층
    public ItemDatabase itemDB;
    SoundManager sound;
    public ShopBtn itrBtn;
    public GameObject ClearMsg;

    void Awake()
    {
        if (!GMExists)
        {
            GMExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _instance = this;
    }
    void Start()

    {
        // 씬에서 오브젝트를 찾아내 연결
        player = FindObjectOfType<Player>();
        inven = Inventory.instance;
        unique = Unique.instance;
        sound = GetComponentInChildren<SoundManager>();

    }
    void Update()
    {
        // 다음 씬으로 이동하면 해당 씬의 오디오소스를 새로 갱신
        if (stage != SceneManager.GetActiveScene().name)
        {
            stage = SceneManager.GetActiveScene().name;
            isBossStage();
            sound.FindSound();
            if (stage == "home" && curArea > 2)
                Instantiate(ClearMsg);
        }

        IsShop(); // home씬에 들어서면 상점을 버튼과 연결함
        SelfDestroy(); // 인게임에서 시작화면으로 돌아올때 게임매니저를 파괴

        // 플레이어가 죽을 것을 확인하면 home씬으로 이동
        if (player.isDead) StartCoroutine(PlayerDie());
        
        
    }
    // Update에서 플레이어의 변화에 따른 상태를 갱신하기 위해 LateUpdate에서 실행
    void LateUpdate()
    {
        UIupdate();
    }
    //전체적인 인게임 UI를 실시간으로 갱신
    void UIupdate()
    {
        // UI의 텍스트를 현상태에 맞게 갱신
        stageTxt.text = "STAGE " + stage;

        playerLevelTxt.text = "Lv." + player.level;
        playerHealthTxt.text = player.health + " / " + player.maxhealth;
        playerCoinTxt.text = string.Format("{0:n0}", inven.curCoin);
        playerExpTxt.text = player.exp + " / " + player.maxExp;

        statusHealthTxt.text = player.health + " / " + player.maxhealth;
        statusDamageTxt.text = player.damage.ToString();
        statusArmorTxt.text = player.armor.ToString();
        statusSpeedTxt.text = player.maxSpeed.ToString();

        //무기 이미지를 갱신후
        quickSlotUpdate();
        //여기서 4는 무기의 최대갯수
        for (int i = 0; i < 4; i++)
        {
            //무기가 최대갯수가 아니더라도 비어있는 창은 투명
            if (i < inven.equipWeapon.Count)
            {
                quickSlot[i].color = new Color(1, 1, 1, inven.equipWeapon[i].itemName != null ? 1 : 0);
                invenQuickSlot[i].color = new Color(1, 1, 1, inven.equipWeapon[i].itemName != null ? 1 : 0);
            }
            //i가 무기갯수를 넘어가면 다 투명으로 만듬
            else
            {
                quickSlot[i].color = new Color(1, 1, 1, 0);
                invenQuickSlot[i].color = new Color(1, 1, 1, 0);
                weaponCntTxt[i].text = "-"; // 빈 무기칸의 사용횟수표기도 초기화
            }
        }
        // 인벤토리 고유아이템 슬롯
        for (int i = 0; i < 8; i++)
        {
            if (i < inven.uniqueitems.Count)
                uniqueSlot[i].color = new Color(1, 1, 1, inven.uniqueitems[i].itemName != null ? 1 : 0);
            else
                uniqueSlot[i].color = new Color(1, 1, 1, 0);
        }
    }
    // 조건에 맞으면 스스로 파괴시킴
    void SelfDestroy()
    {
        // 현재 씬이 시작화면이면 GameManager를 파괴
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            GMExists = false;
            Destroy(gameObject);
        }
    }
    // UI의 상호작용버튼과 상점 패널의 연결
    void IsShop()
    {
        // 현재 씬이 home 씬이면 home의 shop 오브젝트와 UI를 연결
        if (SceneManager.GetActiveScene().name == "home")
            itrBtn.ShopPanel = GameObject.Find("ShopUI").transform.Find("Shop Panel").gameObject;
        else
            itrBtn.ShopPanel = null;
    }
    // UI중 퀵슬롯의 이미지와 사용횟수는 갱신
    void quickSlotUpdate()
    {
        // 인벤토리 무기에 저장된 무기갯수만큼만 이미지 업데이트
        for (int i = 0; i < inven.equipWeapon.Count; i++)
        {
            if (inven.equipWeapon[i].itemName != null)
            {
                //UI 슬롯과 인벤토리 정보를 일치시킴
                Sprite img = Resources.Load<Sprite>("ItemImage/" + inven.equipWeapon[i].itemName);
                quickSlot[i].sprite = img;
                quickSlot[i].preserveAspect = true;
                invenQuickSlot[i].sprite = img;
                invenQuickSlot[i].preserveAspect = true;
                weaponCntTxt[i].text = inven.equipWeapon[i].itemCount < 100 ?
                                        inven.equipWeapon[i].itemCount.ToString() : "-";
            }
        }

        for (int i = 0; i < inven.uniqueitems.Count; i++)
        {
            if (inven.uniqueitems[i].itemName != null)
            {
                //UI 슬롯과 인벤토리 정보를 일치시킴
                Sprite img = Resources.Load<Sprite>("ItemImage/" + inven.uniqueitems[i].itemName);
                uniqueSlot[i].sprite = img;
                uniqueSlot[i].preserveAspect = true;
            }
        }
    }
    //현재 스테이지가 보스 스테이지라면 보스를 탐색
    void isBossStage()
    {
        if (SceneManager.GetActiveScene().name.Contains("Boss"))
        {
            StartCoroutine(FindBoss());
        }
    }

    // 플레이어 사망시 발생하는 루틴
    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1f);
        
        GMExists = false;
        player.PlayerDie();
        SceneLoad.LoadSceneHandle("home", 0);
        Destroy(player.gameObject);
        Destroy(gameObject);
    }
    //보스 탐색 루틴
    IEnumerator FindBoss()
    {
        yield return new WaitForSeconds(0.5f);
        boss = FindObjectOfType<Boss>();
    }
    
    // 저장
    public void Save()
    {
        Debug.Log(Application.persistentDataPath);
        PlayerPrefs.SetInt("curArea", curArea);
        player.SavePlayer();
        unique.SaveUnique();
        inven.SaveInven();
    }

    // 저장
    public void Load()
    {
        Debug.Log(Application.persistentDataPath);
        curArea = PlayerPrefs.GetInt("curArea");
        player.LoadPlayer();
        unique.LoadUnique();
        inven.LoadInven();
    }
    
}
