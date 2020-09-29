﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider2D meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;

    GameObject Arm;
    float ro_angle;
    public void Use() //플레이어가 무기를 사용
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range)
        {
            StopCoroutine("Shot");
            StartCoroutine("Shot");
        }
    }
    IEnumerator Swing()
    {
        Arm = gameObject;
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        for (int i = 0; i < 15; i++)
        {
            yield return null;
            Arm.transform.parent.Rotate(0, 0, -3, Space.Self);
        }
        for (int i = 0; i < 15; i++)
        {
            yield return null;
            Arm.transform.parent.Rotate(0, 0, 3, Space.Self);
        }
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, new Vector2(bulletPos.position.x, bulletPos.position.y), Quaternion.identity);
        Rigidbody2D bulletRigid = instantBullet.GetComponent<Rigidbody2D>();
        Arm = gameObject;

        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        if (gameObject.transform.parent.parent.localScale.x == -1)
            bulletRigid.velocity = bulletPos.right * -10;
        else
            bulletRigid.velocity = bulletPos.right * 10;
        for (int i = 0; i < 15; i++)
        {
            yield return null;
            Arm.transform.parent.Rotate(0, 0, -2, Space.Self);
        }
        for (int i = 0; i < 15; i++)
        {
            yield return null;
            Arm.transform.parent.Rotate(0, 0, 2, Space.Self);
        }

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;
    }
    
    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    //Use() 메인루틴 + Swing() 코루틴 (동시작동)
}
