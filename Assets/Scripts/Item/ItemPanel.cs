using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IPointerClickHandler
{
    public Item slotitem;
    public Button btnEquip;
    public Button btnTrash;
    public Player player;

    void Start()
    {
        Debug.Log(slotitem.itemName);
        player = FindObjectOfType<Player>();
    }
    public void OnClickEquip()
    {
        if(player.hasWeapons[0] == false)
        {

        }
    }
    public void OnClickTrash()
    {

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Destroy(gameObject);
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
