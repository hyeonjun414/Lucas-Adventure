using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {ShotSword, Sword, HeavyWeapon, Spear, Mace}; // 무기의 타입
    public Type type; // 타입에 접근
    public int damage; // 무기 공격력
    public float rate; // 공격 딜레이
    public BoxCollider2D meleeArea; // 근접 무기의 공격 반경
    private void Awake()
    {
        switch (type)
        {
            case Weapon.Type.ShotSword:
                rate = 1;
                break;
            case Weapon.Type.Sword:
                rate = 1;
                break;
            case Weapon.Type.HeavyWeapon:
                rate = 1;
                break;
            case Weapon.Type.Spear:
                rate = 1;
                break;
            case Weapon.Type.Mace:
                rate = 1;
                break;
            default: break;
        }
    }
    public void Use(Item item) //플레이어가 무기를 사용
    {
        StopCoroutine(Swing(item));
        StartCoroutine(Swing(item));
    }
    IEnumerator Swing(Item item)
    {
        // 공격할 때만 무기의 충돌처리를 활성화 시킴
        meleeArea.enabled = true;
        yield return new WaitForSeconds(0.8f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.2f);
        item.itemCount--;
    }
}
