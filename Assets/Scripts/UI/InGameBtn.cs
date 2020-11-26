using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameBtn : MonoBehaviour
{
    public BTNInGame btntype;
    

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public GameObject ManuPanel;

    public GameObject StatusPanel;
    bool activeStatus = false;
    
    public GameObject OptionPanel;
    
    public void OnBtnClick()
    {
        switch (btntype)
        {
            case BTNInGame.Exit:
                SceneManager.LoadScene("StartScene");
                break;
            case BTNInGame.Inventory:
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
                break;
            case BTNInGame.Manu:
                ManuPanel.SetActive(true);
                break;
            case BTNInGame.ManuExit:
                ManuPanel.SetActive(false);
                break;
            case BTNInGame.Status:
                activeStatus = !activeStatus;
                StatusPanel.SetActive(activeStatus);
                break;
           /*
            case BTNInGame.Save:
                activeStatus = !activeStatus;
                StatusPanel.SetActive(activeStatus);
                break;
           */
            case BTNInGame.Option:
                OptionPanel.SetActive(true);
                break;
            case BTNInGame.OptionExit:
                OptionPanel.SetActive(false);
                break;
        }
    }
}
