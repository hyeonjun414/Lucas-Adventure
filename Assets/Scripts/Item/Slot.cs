using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotnum;
    public Item item;
    /*
    public Image itemIcon;
    public GameObject panel;
    public Canvas canvas;
    void Start()
    {
        
        canvas = FindObjectOfType<Canvas>(); 
    }
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.preserveAspect = true;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
    public void OnClick()
    {
        if (item.itemImage == null) return;
        GameObject go = Instantiate(panel, new Vector3(gameObject.transform.position.x + 50, gameObject.transform.position.y - 50, panel.transform.position.z), Quaternion.identity);
        go.transform.SetParent(canvas.transform);
        ItemPanel ip = go.GetComponent<ItemPanel>();
        ip.slotitem = item;
        ip.slotnum = slotnum;
        
        bool isUse = item.Use(item);
        if (isUse)
        {
            Inventory.instance.RemoveItem(slotnum);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
    */
}
