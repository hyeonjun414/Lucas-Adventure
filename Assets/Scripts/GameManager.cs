using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static bool GMExists;
    public Player player;
    public Boss boss;

    // public bool[] clear; //클리어
    //==================//
    public bool Cnt;//보스가 죽으면 카운트
    [SerializeField] private Block block1;
    public static GameManager _instance;
    
    //==============//
    public string stage;

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

    bool activeInventory = false;
    bool activeManu = false;
    
    Inventory inven;

    public ItemDatabase itemDB;
    SoundManager sound;
    public ShopBtn itrBtn;
    public Fade_Manager fm;
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
    }
    void Start()

    {
        Load();//불러오기
        player = FindObjectOfType<Player>();
        inven = Inventory.instance;
        inventoryPanel.SetActive(activeInventory);
        sound = GetComponentInChildren<SoundManager>();
        Cnt = false;

    }
    void Update()
    {
        if (stage != SceneManager.GetActiveScene().name)
        {
            stage = SceneManager.GetActiveScene().name;
            sound.FindSound();
            
        }
        IsShop();
        SelfDestroy();
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeManu = !activeManu;
            ManuPanel.SetActive(activeManu);
        }

        //=============================
        if (Cnt ==true)
        {
            block1.BlockArea();//블록활성화
        }
       
        //=============================

        if (player.isDead) StartCoroutine(PlayerDie());
        
        
    }


    //============================

    public void Bcount()
    {
        Cnt = true;
    }
    //============================

    void quickSlotUpdate()
    {
        // 인벤토리 무기에 저장된 무기갯수만큼만 이미지 업데이트
        for(int i = 0; i<inven.equipWeapon.Count; i++)
        {
            if(inven.equipWeapon[i].itemName != null)
            {
                //인벤토리의 이미지를 받아와 퀵슬롯과 인벤토리무기슬롯에 업데이트
                Sprite img = inven.equipWeapon[i].itemImage;
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
                //인벤토리의 이미지를 받아와 퀵슬롯과 인벤토리무기슬롯에 업데이트
                Sprite img = inven.uniqueitems[i].itemImage;
                uniqueSlot[i].sprite = img;
                uniqueSlot[i].preserveAspect = true;
            }
        }
    }

    void LateUpdate()
    {
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
            if(i < inven.equipWeapon.Count)
            {
                quickSlot[i].color = new Color(1, 1, 1, inven.equipWeapon[i].itemName != null ? 1 : 0);
                invenQuickSlot[i].color = new Color(1, 1, 1, inven.equipWeapon[i].itemName != null ? 1 : 0);
            }
            //i가 무기갯수를 넘어가면 다 투명으로 만듬
            else
            {
                quickSlot[i].color = new Color(1, 1, 1, 0);
                invenQuickSlot[i].color = new Color(1, 1, 1, 0);
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (i < inven.uniqueitems.Count)
            {
                uniqueSlot[i].color = new Color(1, 1, 1, inven.uniqueitems[i].itemName != null ? 1 : 0);
            }
            else
            {
                uniqueSlot[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
    void SelfDestroy()
    {
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            GMExists = false;
            Destroy(gameObject);
        }
    }
    void IsShop()
    {
        if (SceneManager.GetActiveScene().name == "home")
        {
            itrBtn.ShopPanel = GameObject.Find("ShopUI").transform.Find("Shop Panel").gameObject;
        }
        else
        {
            itrBtn.ShopPanel = null;
        }
    }
    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1f);
        
        GMExists = false;
        player.PlayerDie();
        SceneLoad.LoadSceneHandle("home", 0);
        Destroy(player.gameObject);
        Destroy(gameObject);
        
    }

    //==============================//
    public void Save()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x); //Player X축 위치
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y); //Player Y축 위치
        PlayerPrefs.Save();
        
    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        player.transform.position = new Vector3(x, y, 0);
    }
}
