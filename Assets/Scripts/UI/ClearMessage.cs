using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearMessage : MonoBehaviour
{
    public RectTransform image;
    public CanvasGroup canvas;
    void Start()
    {
        canvas = GetComponentInParent<CanvasGroup>();
        image.DOAnchorPos(new Vector2(0, 650), 0.25f);
        StartCoroutine(moveClearMsg());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator moveClearMsg()
    {
        image.DOAnchorPos(new Vector2(0, 200), 1.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2f);
        canvas.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
