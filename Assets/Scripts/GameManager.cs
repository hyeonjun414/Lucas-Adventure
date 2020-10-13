using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy Enemy;
    public int stage;
    public float playTime;
    public bool isBattle;

    public GameObject gamePanel;
    public GameObject inventoryPanel;
    public Text stageTxt;
    public Text playTimeTxt;

    public Text playerHealthTxt;
    public Text playerCoinTxt;
    public Text playerExpTxt;

    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;

    bool activeInventory = false;

    void Awake()
    {

    }
    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
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

    }
}
