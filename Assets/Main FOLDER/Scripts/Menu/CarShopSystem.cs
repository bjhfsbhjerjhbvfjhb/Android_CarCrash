using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarShopSystem : MonoBehaviour
{
    public int saveCarIntPrevious;
    public int carIntChoose;

    public bool[] wasCarPurchase;
    
    public GameObject[] carsObject;

    private readonly int[] carPrice = new[] {0, 200, 200, 400, 450, 500, 500, 500, 700, 1000};
    private readonly string[] carName = new[] {"Ласточка", "Будущее", "Трон", "Старая Школа", "Буханка", "Картофель", "Фарт Фикус", "Погоня", "Ласточка9", "Ласточка10"};

    public Image chooseButtonImage;
    public Text chooseButtonText;
    public Text priceCarText;
    public Text nameCarText;

    private MenuManager menuManager;

    private const string saveKey = "CarSave";

    private void Start()
    {
        menuManager = GetComponent<MenuManager>();
        
        wasCarPurchase = new bool [carsObject.Length];
    }
    
    public void NextCarButton()
    {
        if (carIntChoose < carsObject.Length - 1)
        {
            //DeactivateAllCars();
            
            carIntChoose++;

            StartCoroutine("IE_ChangeCar");
            //CheckPurchasedCar();
        }
    }

    public void PreviousCarButton()
    {
        if (carIntChoose > 0)
        {
            //DeactivateAllCars();
            
            carIntChoose--;

            StartCoroutine("IE_ChangeCar");
            //CheckPurchasedCar();
        }
    }
    
    public void ChooseCarButton()
    {
        chooseButtonText.text = "Выбрано";
        priceCarText.text = "Куплено";
        
        chooseButtonImage.color = Color.green;

        saveCarIntPrevious = carIntChoose;
        
        menuManager.checkCars = carIntChoose;

        wasCarPurchase[carIntChoose] = true;
        SaveManager.Save(saveKey, GetSaveSnapshot());
        
        //PlayerPrefs.SetInt("SaveCar", menuManager.checkCars);
    }

    public void CheckPurchasedCar()
    {
        carsObject[carIntChoose].SetActive(true);

        nameCarText.text = carName[carIntChoose];
        
        if (wasCarPurchase[carIntChoose])
        {
            chooseButtonImage.color = Color.green;
                
            priceCarText.text = "Куплено";
            chooseButtonText.text = "Выбрать";
        }
        else
        {
            chooseButtonImage.color = Color.white;
                
            priceCarText.text = "Цена: " + carPrice[carIntChoose];
            chooseButtonText.text = "Купить";
        }
    }
    
    public void DeactivateAllCars()
    {
        for (int i = 0; i < carsObject.Length; i++)
        {
            carsObject[i].SetActive(false);
        }
    }

    public void CloseCarPanel()
    {
        saveCarIntPrevious = menuManager.checkCars;
        carIntChoose = saveCarIntPrevious;
        
        DeactivateAllCars();
        carsObject[saveCarIntPrevious].SetActive(true);
    }

    public void OpenPanel_Load()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            var data = SaveManager.Load<SaveData.CarShop>(saveKey);
        
            for (int i = 0; i < data.AllCars.Length; i++)
            {
                wasCarPurchase[i] = data.GetCarList(i);
            } 
        }
        else
            wasCarPurchase[0] = true;
        
        CheckPurchasedCar();
    }
    
    public void CheckSaveButton()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }
    
    public void CheckLoadButton()
    {
        var data = SaveManager.Load<SaveData.CarShop>(saveKey);

        for (int i = 0; i < data.AllCars.Length; i++)
        {
            wasCarPurchase[i] = data.GetCarList(i);
        }
    }
    
    private SaveData.CarShop GetSaveSnapshot()
    {
        var data = new SaveData.CarShop();
        
        data.FillCarsList(carsObject.Length);
        
        for (int i = 1; i < data.AllCars.Length; i++)
        {
            data.SetCarList(i, wasCarPurchase[i]);
        }

        data.checkCar = menuManager.checkCars;

        return data;
    }

    public IEnumerator IE_ChangeCar()
    {
        menuManager.carAnimator.SetTrigger("isCarChange");
        yield return new WaitForSeconds(0.14f);
        DeactivateAllCars();
        yield return new WaitForSeconds(0.1f);
        CheckPurchasedCar();
    }
}
