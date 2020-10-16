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
    public float playTime;
    public bool isBattle;

    public GameObject gamePanel;
    public GameObject inventoryPanel;
    public GameObject ManuPanel;

    public Text stageTxt;
    public Text playTimeTxt;

    public Text playerHealthTxt;
    public Text playerCoinTxt;
    public Text playerExpTxt;

    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image posionImg;

    bool activeInventory = false;
    bool activeManu = false;

    public Slot[] slots;
    public Transform slotHolder;
    public Inventory inven;
    void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log("check");
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inven.SlotCnt = 4;
        inventoryPanel.SetActive(activeInventory);
        Debug.Log("check2");

    }

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
        Debug.Log("slotcnt");
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

    void Update()
    {
        if (isBattle)
        {
            playTime += Time.deltaTime;
        }
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
    void LateUpdate()
    {
        stageTxt.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        playTimeTxt.text = string.Format("{0:00}", hour) + ":" +
                           string.Format("{0:00}", min) + ":" + 
                           string.Format("{0:00}", sec);

        playerHealthTxt.text = player.health + " / " + player.maxhealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        playerExpTxt.text = player.exp + " / " + player.maxExp;

        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        posionImg.color = new Color(1, 1, 1, 0);
    }
}
