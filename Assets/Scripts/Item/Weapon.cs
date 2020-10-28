using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum Type {Melee, Range}; // 무기의 타입
    public Type type; // 타입에 접근
    public int damage; // 무기 공격력
    public float rate; // 공격 딜레이
    public BoxCollider2D meleeArea; // 근접 무기의 공격 반경

    protected override void Use() //플레이어가 무기를 사용
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
}
