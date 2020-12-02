using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Player player;
    public Inventory inven;
    public List<Item> unique = new List<Item>();
    public List<Item> potion = new List<Item>();

    public Image[] uniqueSlot;
    public int UniqueSlotNum = 0;
    public Item pickUnique;
    public Image pickUniqueImg;
    public Text pickUniqueName;
    public Text pickUniqueInfo;
    public Text pickUniqueValue;

    public Image[] potionSlot;
    public int PotionSlotNum = 0;
    public Item pickPotion;
    public Image pickPotionImg;
    public Text pickPotionName;
    public Text pickPotionInfo;
    public Text pickPotionValue;

    public Text curCoin;
    private void Awake()
    {
        
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        inven = FindObjectOfType<Inventory>();
        UniqueAdd(ItemType.Unique, "healthUp", 0, 100, "체력 100 증가");
        UniqueAdd(ItemType.Unique, "damageUp", 1, 200, "공격력 10 증가");
        UniqueAdd(ItemType.Unique, "armorUp", 2, 300, "방어력 5 증가");
        UniqueAdd(ItemType.Unique, "speedUp", 3, 200, "이동속도 2 증가");

        PotionAdd(ItemType.Potion, "HP-Potion1", 0,10, "현재 체력 15 회복");
        PotionAdd(ItemType.Potion, "MP-Potion1", 1,10, "현재 마나 15 회복");
        PotionAdd(ItemType.Potion, "EXP-Potion1", 2,50, "현재 경험치 25 상승");
        PotionAdd(ItemType.Potion, "HP-Potion2", 0,20, "현재 체력 30 회복");
        PotionAdd(ItemType.Potion, "MP-Potion2", 1,20, "현재 마나 30 회복");
        PotionAdd(ItemType.Potion, "EXP-Potion2", 2,100, "현재 경험치 50 상승");

        SlotUpdate();
        UniqueInfo(0);
        PotionInfo(0);
    }

    void Update()
    {
        curCoin.text = "현재 코인 : " + inven.curCoin;
    }

    public void SellItem(int value)
    {
        if(value == 0)
        {
            if (inven.curCoin >= pickUnique.itemValue)
            {
                inven.curCoin -= pickUnique.itemValue;
                inven.uniqueitems.Add(pickUnique);
                Debug.Log("구입 성공!");

            }
            else
                Debug.Log("돈이 부족함 - 구입 실패");
        }
        else
        {
            if(inven.curCoin >= pickPotion.itemValue)
            {
                switch (pickPotion.itemCount)
                {
                    case 0:
                        if(player.health != player.maxhealth)
                        {
                            if (pickPotion.itemName == "HP-Potion1")
                            {
                                int temp = player.health + 15;
                                temp = temp > player.maxhealth ? player.maxhealth : temp;
                                player.health = temp;
                                inven.curCoin -= pickPotion.itemValue;
                            }
                            else
                            {
                                int temp = player.health + 30;
                                temp = temp > player.maxhealth ? player.maxhealth : temp;
                                player.health = temp;
                                inven.curCoin -= pickPotion.itemValue;

                            }
                        }
                        else
                        {
                            Debug.Log("최대체력입니다.");
                        }
                        
                        break;
                    case 1: // mp 아직 미구현
                        break;
                    case 2:
                        if (pickPotion.itemName == "EXP-Potion1")
                        {
                            player.exp += 25;
                            inven.curCoin -= pickPotion.itemValue;
                        }
                        else
                        {
                            player.exp += 50;
                            inven.curCoin -= pickPotion.itemValue;
                        }
                        break;
                }
            }
        }
    }

    void SlotUpdate()
    {
        for(int i = 0; i<uniqueSlot.Length; i++)
        {
            Sprite img = Resources.Load<Sprite>("ItemImage/" + unique[i].itemName);
            uniqueSlot[i].sprite = img;
            uniqueSlot[i].preserveAspect = true;
        }
        for (int i = 0; i < potionSlot.Length; i++)
        {
            Sprite img = Resources.Load<Sprite>("ItemImage/" + potion[i].itemName);
            potionSlot[i].sprite = img;
            potionSlot[i].preserveAspect = true;
        }
    }
    public void UniqueInfo(int UniqueSlotNum)
    {
        pickUnique = unique[UniqueSlotNum];
        pickUniqueImg.sprite = Resources.Load<Sprite>("ItemImage/" + unique[UniqueSlotNum].itemName);
        pickUniqueImg.preserveAspect = true;
        pickUniqueName.text = pickUnique.itemName;
        pickUniqueInfo.text = pickUnique.itemInfo;
        pickUniqueValue.text = "가격 : " + pickUnique.itemValue.ToString();
    }
    public void PotionInfo(int PotionSlotNum)
    {
        pickPotion = potion[PotionSlotNum];
        pickPotionImg.sprite = Resources.Load<Sprite>("ItemImage/" + potion[PotionSlotNum].itemName);
        pickPotionImg.preserveAspect = true;
        pickPotionName.text = pickPotion.itemName;
        pickPotionInfo.text = pickPotion.itemInfo;
        pickPotionValue.text = "가격 : " + pickPotion.itemValue.ToString();
    }
    void PotionAdd(ItemType itype, string iName, int iCount, int iValue, string info)
    {
        potion.Add(new Item(itype, iName, iCount, iValue, info));
    }

    void UniqueAdd(ItemType itype, string iName, int iCount, int iValue, string info)
    {
        unique.Add(new Item(itype, iName, iCount, iValue, info));
    }
}
