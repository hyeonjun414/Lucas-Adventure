using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNMain btntype;
    public Transform buttonScale;
    Vector3 defaultScale;

    public GameObject OptionPanel;

    private void Start()
    {
        defaultScale = buttonScale.localScale;

    }
    public void OnBtnClick()
    {
        switch (btntype)
        {
            case BTNMain.New:
                SceneLoad.LoadSceneHandle("home", 0);
                Debug.Log("새 게임");
                break;
            case BTNMain.Continue:
                SceneLoad.LoadSceneHandle("PlayerScene", 0);
                Debug.Log("이어하기");
                break;
            case BTNMain.Option:
                Debug.Log("환경설정");
                OptionPanel.SetActive(true);
                break;
            case BTNMain.OptionExit:
                OptionPanel.SetActive(false);
                break;
            case BTNMain.Quit:
                Debug.Log("나가기");
                Application.Quit();
                break;
        }
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
