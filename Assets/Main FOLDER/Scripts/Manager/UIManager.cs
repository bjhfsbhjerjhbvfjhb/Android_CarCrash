using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{

    public Image heartImage, keysImage, energyImage;
    
    private float scoreTimer = 0f;
    public float scoreTdeg = 2f;
    
    public Text ScoreText,ScoreShadowText, StatusText, StatusShadowText, SpeedText, SpeedShadowText,increaseScoreTextText, EndPriceText;
    public Image BrokenScreenImage;

    private int scoreForSave;

    public Animator increaseScoreTextAnimator;

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

    //singleton implementation
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();
            
            return instance;
        }
    }

    protected UIManager()
    {
    }

    public int score = 0;

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
    
    public void SetScore(int value)
    {
        if (score + value >= 0)
        {
            score += value;
            UpdateScoreText();

            increaseScoreTextText.text = "+" + value;
            increaseScoreTextAnimator.SetTrigger("isShow");
        }
    }

    public void IncreaseScoreTimer(int value)
    {
        if (Time.time > scoreTimer + scoreTdeg)
        {
            scoreTimer = Time.time;
            score += value;
            UpdateScoreText();
        }
    }

    public void IncreaseScore(int value)
    {
        if (score + value >= 0)
        {
            score += value;
            UpdateScoreText();  
        }
    }
    
    private void UpdateScoreText()
    {
        ScoreText.text = score.ToString();
        ScoreShadowText.text = ScoreText.text;
    }

    public void SetStatus(string text)
    {
        StatusText.text = text;
        StatusShadowText.text = StatusText.text;
    }

    public void SaveScore()
    {
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            int backSaveScore = PlayerPrefs.GetInt("SaveScore");
            int newScore = backSaveScore + scoreForSave;
            
            PlayerPrefs.SetInt("SaveScore", newScore);
        }
        else
        {
            PlayerPrefs.SetInt("SaveScore", scoreForSave);
        }
    }

    public IEnumerator smoothEnergyAdd(float count)
    {
        float endCount = energyImage.fillAmount + count;
        
        while (energyImage.fillAmount < endCount)
        {
            energyImage.fillAmount = Mathf.MoveTowards(energyImage.fillAmount, endCount, Time.deltaTime * 2f);
            
            yield return null;
        }
    }

    private int ExchangeScoreToPrice(int value)
    {
        return (value / 30) + Random.Range(0, value / 50);
    }

    public IEnumerator smoothScoreToPrice()
    {
        int endPrice = ExchangeScoreToPrice(score);
        int startPrice = 0;

        scoreForSave = endPrice;
        
        while (startPrice < endPrice)
        {
            if (startPrice + 20 < endPrice)
            {
                startPrice += 20;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                startPrice++;
            }
            
            EndPriceText.text = score + " = " + startPrice + " $";
            
            yield return null;
        }
    }
}
