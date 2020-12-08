using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    ItemDatabase itemDB;


    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemType = _item.itemType;
        item.itemCount = _item.itemCount;
        image.sprite = Resources.Load<Sprite>("ItemImage/" + item.itemName);
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
    public void BossDrop()
    {
        itemDB = FindObjectOfType<ItemDatabase>();
        Item getItem = itemDB.unique[Random.Range(0, itemDB.unique.Count)];
        SetItem(getItem);
    }
    public void EnemyDrop()
    {
        itemDB = FindObjectOfType<ItemDatabase>();
        Item getItem = itemDB.items[Random.Range(0, itemDB.items.Count)];
        SetItem(getItem);
    }
}




