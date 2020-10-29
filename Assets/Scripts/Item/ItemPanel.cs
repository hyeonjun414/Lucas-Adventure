using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IPointerClickHandler
{
    public Item slotitem;
    public Button btnEquip;
    public Button btnDiscard;
    public Player player;
    public int slotnum;
    Text txtEquip;
    Text txtDiscard;
    Inventory inven;
    //bool isSwitch = false;

    void Start()
    {
        Debug.Log(slotitem.itemName);
        player = FindObjectOfType<Player>();
        inven = FindObjectOfType<Inventory>();
        txtEquip = btnEquip.GetComponentInChildren<Text>();
        txtDiscard = btnDiscard.GetComponentInChildren<Text>();

    }
    public void OnClickEquip()
    {
    }

    public void OnClickDiscard()
    {
        //Inventory.instance.RemoveItem(slotnum);
    }
    

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Destroy(gameObject, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
