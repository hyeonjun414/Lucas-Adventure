using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearMessage : MonoBehaviour
{
    public RectTransform image;
    void Start()
    {
        image.DOAnchorPos(new Vector2(0, 650), 0.25f);
        moveClearMsg();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void moveClearMsg()
    {
        image.DOAnchorPos(new Vector2(0, 200), 2f).SetEase(Ease.OutBounce);
    }
}
