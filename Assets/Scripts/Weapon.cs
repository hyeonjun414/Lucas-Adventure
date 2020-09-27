using System.Collections;
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
        if (gameObject.transform.parent.parent.localScale.x == -1)
            ro_angle = 15;
        else
            ro_angle = -15;
        Arm.transform.parent.rotation = Quaternion.Euler(0,0,ro_angle*3);
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, ro_angle*6);
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, ro_angle*3);
        yield return new WaitForSeconds(0.1f);
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, 0);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        
        GameObject instantBullet = Instantiate(bullet, new Vector2(bulletPos.position.x, bulletPos.position.y), Quaternion.identity);
        Rigidbody2D bulletRigid = instantBullet.GetComponent<Rigidbody2D>();
        bulletRigid.velocity = bulletPos.right * 10;
        Arm = gameObject;
        if (gameObject.transform.parent.parent.localScale.x == -1)
            ro_angle = 15;
        else
            ro_angle = -15;
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, ro_angle * 1);
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, ro_angle * 2);
        yield return new WaitForSeconds(0.1f);
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, ro_angle * 1);
        yield return new WaitForSeconds(0.1f);
        Arm.transform.parent.rotation = Quaternion.Euler(0, 0, 0);
        trailEffect.enabled = false;
    }
    
    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    //Use() 메인루틴 + Swing() 코루틴 (동시작동)
}
