using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickM : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    RectTransform rect;
    Vector2 touch = Vector2.zero;

    public RectTransform handle;
    public JoystickValue value;

    float widthHalf;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        widthHalf = rect.sizeDelta.x * 0.5f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        eventData.position = new Vector2(eventData.position.x - ((Screen.width - 1920)/2f), eventData.position.y - ((Screen.height - 1080)/2f));
        touch = (eventData.position - rect.anchoredPosition) / widthHalf;

        if (touch.magnitude > 1)
            touch = touch.normalized;
        value.joyTouch = touch;
        handle.anchoredPosition = touch * widthHalf;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        value.joyTouch = Vector2.zero;
    }
}
