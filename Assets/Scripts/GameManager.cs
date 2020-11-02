using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public int stage;

    public GameObject Event;
    public GameObject UI;
    public GameObject gamePanel;
    public GameObject inventoryPanel;
    public GameObject ManuPanel;
    public Text stageTxt;
    public Text playTimeTxt;

    public Text playerLevelTxt;
    public Text playerHealthTxt;
    public Text playerCoinTxt;
    public Text playerExpTxt;


    public Image[] quickSlot;
    public Image[] invenQuickSlot;

    bool activeInventory = false;
    bool activeManu = false;

    
    Inventory inven;

    public ItemDatabase itemDB;
    void Awake()
    {
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(Event);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        inven = Inventory.instance;
        //inven.onSlotCountChange += SlotChange;
        //inven.onChangeItem += RedrawSlotUI;
        //inven.SlotCnt = 4;
        inventoryPanel.SetActive(activeInventory);

    }
    /*
    private void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i< slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }
    */
    void Update()
    {
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
    }
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
            }
        }
    }

    void LateUpdate()
    {
        stageTxt.text = "STAGE " + stage;

        playerLevelTxt.text = "Lv." + player.level;
        playerHealthTxt.text = player.health + " / " + player.maxhealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        playerExpTxt.text = player.exp + " / " + player.maxExp;

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
    }
}
