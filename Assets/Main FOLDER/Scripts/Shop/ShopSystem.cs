using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public Animator shopPanelAnimator;
    public CraftSystem craftSystem;

    public int sumPrice = 0;
    public bool isChooseUpgrade = false;
    
    public Text SpeedStatsText, JumpStatsText, MaterialStatsText, BonusStatsText, SumPriceText;
    public void BackMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SetCraftAnimator()
    {
        shopPanelAnimator.SetBool("isCraft", true);
        shopPanelAnimator.SetBool("isShop", false);
        craftSystem.UpdateAllCount();
    }
    
    public void SetShopAnimator()
    {
        shopPanelAnimator.SetBool("isCraft", false);
        shopPanelAnimator.SetBool("isShop", true);
    }

    public void SpeedUpgrade_1()
    {
        SpeedStatsText.text = "Скорость: +80 км/ч";
        SumPriceText.text = "Стоимость: 1200 валюты";
        isChooseUpgrade = true;
    }
    
    public void SpeedUpgrade_2()
    {
        SpeedStatsText.text = "Скорость: +140 км/ч";
        SumPriceText.text = "Стоимость: 2600 валюты";
        isChooseUpgrade = true;
    }
    
    public void EngineUpgrade_1()
    {
        JumpStatsText.text = "Высота прыжка: +2 м";
        SumPriceText.text = "Стоимость: 1500 валюты";
        isChooseUpgrade = true;
    }
    
    public void EngineUpgrade_2()
    {
        JumpStatsText.text = "Высота прыжка: +4 м";
        SumPriceText.text = "Стоимость: 4000 валюты";
        isChooseUpgrade = true;
    }
    
    public void GearUpgrade_1()
    {
        MaterialStatsText.text = "Сбор материалов: x1.2";
        BonusStatsText.text = "Бонус: x1.5";
        SumPriceText.text = "Стоимость: 5200 валюты";
        isChooseUpgrade = true;
    }
    
    public void GearUpgrade_2()
    {
        MaterialStatsText.text = "Сбор материалов: x1.4";
        BonusStatsText.text = "Бонус: x2";
        SumPriceText.text = "Стоимость: 9000 валюты";
        isChooseUpgrade = true;
    }

    public void BuyShopButton()
    {
        if (isChooseUpgrade)
        {
            shopPanelAnimator.SetTrigger("isPopUp");
        }
    }
}
