using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text scoreText;
    public Text scoreTextBackground;

    private int loadScore;
    
    public GameObject mainPanel;

    public Camera mainCamera;

    public Animator cameraAnimator;
    public Animator carAnimator;
    public Animator policeAnimator;
    public Animator playPanelAnimator;
    public Animator blackBorderAnimator;

    public MenuSoundManager menuSoundManager;

    public int checkCars;
    private CarShopSystem carShopSystem;

    public bool[] carRealBuy;

    private const string carSaveKey = "CarSave";
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        carShopSystem = GetComponent<CarShopSystem>();
        
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            PlayerPrefs.GetInt("SaveScore", loadScore);
            Debug.Log(PlayerPrefs.GetInt("SaveScore"));
            
            scoreText.text = PlayerPrefs.GetInt("SaveScore").ToString();
            //scoreTextBackground.text = scoreText.text;
        }
        else
        {
            loadScore = 0;
            Debug.Log(loadScore);
        }
        
        //Загрузка сохраненной машины
        if (PlayerPrefs.HasKey("CarSave"))
        {
            //checkCars = PlayerPrefs.GetInt("SaveCar");
            var data = SaveManager.Load<SaveData.CarShop>("CarSave");
            checkCars = data.checkCar;
            carShopSystem.carIntChoose = checkCars;
            carShopSystem.DeactivateAllCars();
            carShopSystem.carsObject[carShopSystem.carIntChoose].SetActive(true);
        }
        else
        {
            carShopSystem.carIntChoose = 0;
            carShopSystem.DeactivateAllCars();
            carShopSystem.carsObject[carShopSystem.carIntChoose].SetActive(true);
        }
    }

    public void SmoothCameraOpenCarPanel()
    {
        StartCoroutine(CameraZoom(60f));
        blackBorderAnimator.SetBool("isOpen", true);
    }
    
    public void SmoothCameraCloseCarPanel()
    {
        StartCoroutine(CameraUnZoom(70f));
        blackBorderAnimator.SetBool("isOpen", false);
    }

    public void PlayButton()
    {
        mainPanel.SetActive(false);
        menuSoundManager.PlayOneShot(menuSoundManager.playButtonClip);
        carAnimator.SetTrigger("isRun");
        policeAnimator.SetTrigger("isRun");
        StartCoroutine("PlaySmooth");
    }
    
    private IEnumerator PlaySmooth()
    {
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("GameScene");
    }

    public void OpenCaseButton()
    {
        cameraAnimator.SetTrigger("transitionToChest");
        StartCoroutine("OpenCaseSmooth");
    }

    private IEnumerator OpenCaseSmooth()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuScene_OpenCase");
    }

    public void OpenShopButton()
    {
        cameraAnimator.SetTrigger("transitionToWorkShop");
        StartCoroutine("OpenShopSmooth");
    }

    public void PlayPanel_ShowHide(bool isActive)
    {
        playPanelAnimator.SetBool("isShow", isActive);
    }
    
    private IEnumerator OpenShopSmooth()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuScene_Shop");
    }
    
    private IEnumerator CameraZoom(float value)
    {
        while (mainCamera.fieldOfView > value)
        {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, value, Time.deltaTime * 40f);
            
            yield return null;
        }
    }
    
    private IEnumerator CameraUnZoom(float value)
    {
        while (mainCamera.fieldOfView < value)
        {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, value, Time.deltaTime * 40f);
            
            yield return null;
        }
    }
}
