using System.Collections;
using Lean.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerHealth = 3;
    public int playerEnergy = 0;

    public GameObject deadPanel_Obj;
    public GameObject player_Obj;
    public Camera playerCamera_Obj;

    public GameObject deadPanel;
    public GameObject gamePanel;

    public GameObject trailRedLine_Obj;
    public GameObject particleSpeed_Obj;

    public GameObject startLocationObject;
    public GameObject fadeTransitionObject;
    private Animator fadeTransitionAnimator;
    public Animator speedUpButtonAnimator;

    public GameObject TapTapMiniGame_Panel;
    public GameObject CasinoMiniGame_Panel;
    
    public Color damageBrokeScreenColor;
    public Color normalBrokeScreenColor;
    public float fadeBrokeScreenTime = 0.1f;

    public UIManager uiManager;
    public SoundManager soundManager;

    public GameObject[] carsObject;
    public Transform spawnCarPoint;

    public Transform deadSpawnCarPoint;
    public int checkCar;

    public Shaker shaker;
    public PlayerMove playerMove;

    public bool canDamage;
    public bool isCasinoExit;

    private void Start()
    {
        fadeTransitionAnimator = fadeTransitionObject.GetComponent<Animator>();
        
        playerHealth = 3;

        if (PlayerPrefs.HasKey("CarSave"))
        {
            var data = SaveManager.Load<SaveData.CarShop>("CarSave");
            checkCar = data.checkCar;
            GameObject player = Instantiate(carsObject[checkCar], spawnCarPoint.transform);
            
            if (checkCar == 2)
            {
                playerMove.trailRendererWheel.material = playerMove.trailRender_bike;
            }
            else
            {
                playerMove.trailRendererWheel.material = playerMove.trailRender_default;
            }
        }
        else
        {
            GameObject player = Instantiate(carsObject[0], spawnCarPoint.transform);
        }

        if (GameState == GameState.Start)
        {
            StartCoroutine("SetUnFade");
        }
    }

    #region Instance

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    #endregion

    protected GameManager()
    {
        GameState = GameState.Start;
        CanSwipe = false;
    }

    public GameState GameState { get; set; }

    public bool CanSwipe { get; set; }

    public void Damage(int count)
    {
        if (playerHealth - count > 0)
        {
            if (!canDamage)
            {
                playerHealth--;
                UIManager.Instance.heartImage.fillAmount -= 0.34f;
            }
            SoundManager.Instance.PlayOneShot(soundManager.carDamage);
            CallingFadeScreenDamageMethod();
        }
        else
        {
            Die();
        }
    }

    public void Heal()
    {
        if (playerHealth > 0 && playerHealth < 3)
        {
            playerHealth++;
            UIManager.Instance.heartImage.fillAmount += 0.34f;
        }
    }

    public void EnergyUP()
    {
        if (playerEnergy < 4)
        {
            playerEnergy++;
            StartCoroutine(UIManager.Instance.smoothEnergyAdd(0.25f));

            if (playerEnergy >= 4)
            {
                speedUpButtonAnimator.SetBool("isShow", true);
            }
        }
    }

    public void EnergyClear()
    {
        if (playerEnergy >= 4)
        {
            playerEnergy = 0;
            
            speedUpButtonAnimator.SetBool("isShow", false);
            
            UIManager.Instance.energyImage.fillAmount = 0f;
            StartCoroutine("EnergyRoadForward");
        }
    }
    
    private IEnumerator EnergyRoadForward()
    {
        RoadSpawnSystem.Instance.onlyForward = true;
        playerMove.speedUpgrade += 0.4f;
        playerCamera_Obj.GetComponent<Camera>().fieldOfView += 5f;
        yield return new WaitForSeconds(8f);
        RoadSpawnSystem.Instance.onlyForward = false;
        playerMove.speedUpgrade -= 0.4f;
        playerCamera_Obj.GetComponent<Camera>().fieldOfView -= 5f;
    }
    
    public void Die()
    {
            UIManager.Instance.SetStatus(Constants.StatusDeadTapToStart);
            GameState = GameState.Dead; 
            LeanPool.DespawnAll();
            
            GameObject player = Instantiate(carsObject[checkCar], deadSpawnCarPoint.transform);
            
            deadPanel_Obj.SetActive(true);
            
            player_Obj.SetActive(false);
            playerCamera_Obj.enabled = false;
            
            deadPanel.SetActive(true);
            gamePanel.SetActive(false);

            StartCoroutine(UIManager.Instance.smoothScoreToPrice());
            
            uiManager.SaveScore();
    }

    public void BackMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private IEnumerator SetUnFade()
    {
        fadeTransitionObject.SetActive(true);
        fadeTransitionAnimator.SetTrigger("isUnFade");
        yield return new WaitForSeconds(1.2f);
        fadeTransitionObject.SetActive(false);
    }
    
    void CallingFadeScreenDamageMethod()
    {
        uiManager.BrokenScreenImage.color = damageBrokeScreenColor;
        shaker.Shake(0.1f);
        StartCoroutine("FadeOut");
    }
    
    private IEnumerator FadeOut()
    {
        while (uiManager.BrokenScreenImage.color.a > 0)
        {
            uiManager.BrokenScreenImage.color = Color.Lerp(uiManager.BrokenScreenImage.color, normalBrokeScreenColor, fadeBrokeScreenTime * Time.deltaTime);
            if (uiManager.BrokenScreenImage.color.a <= 0.1f)
            {
                uiManager.BrokenScreenImage.color = normalBrokeScreenColor;
            }
            yield return null;
        }
    }
}



