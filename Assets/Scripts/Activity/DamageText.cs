﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Text Mesh Pro 사용을 위해 임포트

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    TextMeshPro text;
    Color alpha;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        DamageStart();
    }

    // Update is called once per frame
    void Update()
    {
        DamageUpdate();
    }
    private void DestroyText()
    {
        Destroy(gameObject);
    }

    void DamageStart()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyText", destroyTime);
    }
    void DamageUpdate()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }
}
