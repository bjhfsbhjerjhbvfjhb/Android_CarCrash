using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
    public int money;
    
    //Resources
    public int RWood_Count;
    public int ROil_Count;
    public int RCoal_Count;
    public int RIron_Count;
    public int RMetal_Count;
    public int RGear_Count;
    public int RPart_Count;
    public int RScheme_Count;
    public int RPlate_Count;
    
    //UI
    public Text[] countsText;

    public void CraftCoalButton()
    {
        if (RWood_Count >= 4 && ROil_Count >= 1 && money >= 50)
        {
            RWood_Count -= 4;
            ROil_Count -= 1;
            money -= 50;
            RCoal_Count += Random.Range(10, 20);
        }
        
        UpdateAllCount();
    }
    
    public void CraftMetalButton()
    {
        if (RIron_Count >= 4 && RCoal_Count >= 5 && money >= 100)
        {
            RIron_Count -= 4;
            RCoal_Count -= 5;
            money -= 100;
            RMetal_Count += Random.Range(2, 6);
        }
        
        UpdateAllCount();
    }
    
    public void CraftGearButton()
    {
        if (RMetal_Count >= 10 && RCoal_Count >= 5 && money >= 100)
        {
            RMetal_Count -= 10;
            RCoal_Count -= 5;
            money -= 100;
            RGear_Count += Random.Range(1, 2);
        }
        
        UpdateAllCount();
    }
    
    public void CraftPartButton()
    {
        
    }
    
    public void CraftSchemeButton()
    {
        
    }
    
    public void CraftPlateButton()
    {
        
    }

    public void UpdateAllCount()
    {
        //Coal
        countsText[0].text = RWood_Count.ToString() + " / 4";
        countsText[1].text = ROil_Count.ToString() + " / 1";
        countsText[2].text = money.ToString() + " / 50";
        
        //Metal
        countsText[3].text = RIron_Count.ToString() + " / 4";
        countsText[4].text = RCoal_Count.ToString() + " / 5";
        countsText[5].text = money.ToString() + " / 100";
        
        //Gear
        countsText[6].text = RMetal_Count.ToString() + " / 10";
        countsText[7].text = RCoal_Count.ToString() + " / 5";
        countsText[8].text = money.ToString() + " / 100";
        
        //Part
        countsText[9].text = RMetal_Count.ToString() + " / 10";
        countsText[10].text = RGear_Count.ToString() + " / 5";
        countsText[11].text = money.ToString() + " / 200";
        
        //Scheme
        countsText[12].text = RGear_Count.ToString() + " / 4";
        countsText[13].text = RPart_Count.ToString() + " / 5";
        countsText[14].text = money.ToString() + " / 300";
        
        //Plate
        countsText[15].text = RScheme_Count.ToString() + " / 5";
        countsText[16].text = RPart_Count.ToString() + " / 5";
        countsText[17].text = money.ToString() + " / 500";
    }
}
