using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public Text mytext;

    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public GameObject ManuPanel;
    bool activeManu = false;

    public GameObject StatusPanel;
    bool activeStatus = false;

    public GameObject ShopPanel;

    bool isSound = true;

    private void Start()
    {   
        defaultScale = buttonScale.localScale;
        
    }
    public void OnBtnClick()
    {
        /*
        switch (currentType)
        {
            case BTNType.New:
                SceneLoad.LoadSceneHandle("home", 0);
                Debug.Log("새 게임");
                break;
            case BTNType.Continue:
                SceneLoad.LoadSceneHandle("PlayerScene", 0);
                Debug.Log("이어하기");
                break;
            case BTNType.Option:
                Debug.Log("환경설정");
                break;
            case BTNType.Sound:
                break;
            case BTNType.Back:
                Debug.Log("뒤로가기");
                break;
            case BTNType.Quit:
                Debug.Log("나가기");
                Application.Quit();
                break;
            case BTNType.InGameExit:
                SceneManager.LoadScene("StartScene");
                break;
            case BTNType.Inventory:
                activeInventory = !activeInventory;
                inventoryPanel.SetActive(activeInventory);
                break;
            case BTNType.Manu:
                activeManu = !activeManu;
                ManuPanel.SetActive(activeManu);
                break;
            case BTNType.Status:
                activeStatus = !activeStatus;
                StatusPanel.SetActive(activeStatus);
                break;

        }*/
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
