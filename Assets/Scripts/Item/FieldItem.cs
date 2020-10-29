using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    ItemDatabase itemDB;

    private void Start()
    {
        itemDB = FindObjectOfType<ItemDatabase>();
        Item getItem = itemDB.items[Random.Range(0, itemDB.items.Count)];
        SetItem(getItem);
    }

    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        image.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}




