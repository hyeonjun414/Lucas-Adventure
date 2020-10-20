using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range}; // 무기의 타입
    public Type type; // 타입에 접근
    public int damage; // 무기 공격력
    public float rate; // 공격 딜레이
    public BoxCollider2D meleeArea; // 근접 무기의 공격 반경
    public GameObject bullet; // 원거리 무기가 사용할 탄알오브젝트
    Transform bulletPos;
    GameObject Arm;
    float ro_angle;
    void Start()
    {
        // 플레이어 위치는 탄알 발사위치로 선정
        bulletPos = FindObjectOfType<Player>().transform;
    }
    public void Use() //플레이어가 무기를 사용
    {
        // 무기가 근접일 경우 Swing 코루틴 실행
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        // 무기가 원거리일 경우 Shot 코루틴 실행
        else if(type == Type.Range)
        {
            StopCoroutine("Shot");
            StartCoroutine("Shot");
        }
    }
    IEnumerator Swing()
    {
        // 공격할 때만 무기의 충돌처리를 활성화 시킴
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator Shot()
    {
        // 탄알 오브젝트를 생성하고 일정한 속도로 발사하게함
        GameObject instantBullet = Instantiate(bullet, new Vector2(bulletPos.position.x, bulletPos.position.y), Quaternion.identity);
        Rigidbody2D bulletRigid = instantBullet.GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(0.1f);
        if (gameObject.transform.parent.parent.localScale.x == -1)
            bulletRigid.velocity = bulletPos.right * -10;
        else
            bulletRigid.velocity = bulletPos.right * 10;

        yield return new WaitForSeconds(0.3f);
    }
    
    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    //Use() 메인루틴 + Swing() 코루틴 (동시작동)
}
