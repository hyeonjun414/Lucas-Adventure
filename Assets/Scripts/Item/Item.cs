using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Unique,
    Weapon,
    posion,
    coin
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public GameObject item;

    protected virtual void Use()
    {

    }
}
