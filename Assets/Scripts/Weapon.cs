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
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, new Vector2(bulletPos.position.x, bulletPos.position.y), Quaternion.identity);
        Rigidbody2D bulletRigid = instantBullet.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;
        if (gameObject.transform.parent.parent.localScale.x == -1)
            bulletRigid.velocity = bulletPos.right * -10;
        else
            bulletRigid.velocity = bulletPos.right * 10;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    
    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    //Use() 메인루틴 + Swing() 코루틴 (동시작동)
}
