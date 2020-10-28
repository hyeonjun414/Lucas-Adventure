using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.AngleAxis(30, new Vector3(0, 0, -1)));
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 20)]);
        }
    }
    public void DropItem(Transform pos)
    {
        GameObject go = Instantiate(fieldItemPrefab, 
                        new Vector3(pos.position.x,
                                    pos.transform.position.y,
                                    pos.transform.position.z), Quaternion.AngleAxis(30, new Vector3(0, 0, -1)));
        go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 20)]);
    }
}
