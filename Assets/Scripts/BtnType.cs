using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public Text mytext;

    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    bool isSound = true;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                SceneLoad.LoadSceneHandle("PlayScene", 0);
                Debug.Log("새 게임");
                break;
            case BTNType.Continue:
                SceneLoad.LoadSceneHandle("PlayScene", 1);
                Debug.Log("이어하기");
                break;
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                Debug.Log("환경설정");
                break;
            case BTNType.Sound:
                if (isSound)
                {
                    isSound = !isSound;
                    mytext.text = "음향 꺼짐";
                    Debug.Log("음향 끄기");
                }
                else
                {
                    isSound = !isSound;
                    mytext.text = "음향 켜짐";
                    Debug.Log("음향 켜기");
                }
                    
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                Debug.Log("뒤로가기");
                break;
            case BTNType.Quit:
                Debug.Log("나가기");
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
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
