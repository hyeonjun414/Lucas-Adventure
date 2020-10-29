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
        /*
        for(int i = 0; i<player.weapons.Length; i++)
        {
            if(player.hasWeapons[i])
            {
                Sprite img = player.weapons[i].itemImage;
                quickSlot[i].sprite = img;
                quickSlot[i].preserveAspect = true;
                invenQuickSlot[i].sprite = img;
                invenQuickSlot[i].preserveAspect = true;
            }
        }*/
    }

    void LateUpdate()
    {
        stageTxt.text = "STAGE " + stage;

        playerLevelTxt.text = "Lv." + player.level;
        playerHealthTxt.text = player.health + " / " + player.maxhealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        playerExpTxt.text = player.exp + " / " + player.maxExp;

        /*
        quickSlotUpdate();
        for (int i = 0; i < quickSlot.Length; i++)
        {
            quickSlot[i].color = new Color(1, 1, 1, player.hasWeapons[i] ? 1 : 0);
            invenQuickSlot[i].color =  new Color(1, 1, 1, player.hasWeapons[i] ? 1 : 0);
        }
        invenQuickSlot[4].color = new Color(1, 1, 1, player.hasArmor ? 1 : 0);
        */
    }
}
