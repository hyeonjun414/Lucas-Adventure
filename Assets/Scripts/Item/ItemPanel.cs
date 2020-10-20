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
    bool isSwitch = false;

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
        if (isSwitch == false)
        {
            bool isUse = slotitem.Use(slotitem, slotnum);
            if (isUse)
            {
                Inventory.instance.RemoveItem(slotnum);
            }
        }
            
    }

    public void OnClickDiscard()
    {
        Inventory.instance.RemoveItem(slotnum);
    }

    public void OnClickSwitch()
    {
        bool isUse = slotitem.SwitchWeapon(slotitem, slotnum);
        if (isUse)
        {
            Inventory.instance.RemoveItem(slotnum);
        }
    }
    public void OnClickSwitch2()
    {
        bool isUse = slotitem.Use(slotitem, slotnum);
        if (isUse)
        {
            Inventory.instance.RemoveItem(slotnum);
        }
    }


    void Update()
    {
        if (player.hasWeapons[0] == true && player.hasWeapons[1] == true)
        {
            isSwitch = true;
            txtEquip.text = "교체하기";
        }
        else
            isSwitch = false;
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
