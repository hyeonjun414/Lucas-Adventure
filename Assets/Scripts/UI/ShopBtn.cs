using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopBtn : MonoBehaviour
{
    public BTNShop currentType;

    public Shop shop;
    public GameObject ShopPanel;
    public GameObject uniqueTab;
    public GameObject potionTab;

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNShop.Shop:
                ShopPanel.SetActive(true);
                break;
            case BTNShop.ShopExit:
                ShopPanel.SetActive(false);
                shop.UniqueInfo(0);
                shop.PotionInfo(0);
                break;
            case BTNShop.UniqueTab:
                uniqueTab.SetActive(true);
                potionTab.SetActive(false);
                break;
            case BTNShop.PotionTab:
                uniqueTab.SetActive(false);
                potionTab.SetActive(true);
                break;
            case BTNShop.BuyItem:
                if (uniqueTab.activeSelf)
                    shop.SellItem(0);
                else
                    shop.SellItem(1);

                break;



        }
    }
}
