using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TapTap_MG : MonoBehaviour
{
    public Scrollbar circleMain_ScrollBar;
    public PlayerMove playerMove;
    private float speedUpgrade;
    private bool isTapTap_MG_Active;

    private void Start()
    {
        speedUpgrade = playerMove.speedUpgrade;
    }

    public void TapTapButton()
    {
        UIManager.Instance.SetScore(Random.Range(20, 50));
        if (circleMain_ScrollBar.value - 0.025f > 0f)
        {
            circleMain_ScrollBar.value -= 0.025f;
        }
        else
        {
            circleMain_ScrollBar.value -= 0f + circleMain_ScrollBar.value;
        }
    }

    private void OnEnable()
    {
        isTapTap_MG_Active = true;
        circleMain_ScrollBar.value = 0f;
        
        float time = Random.Range(0.5f, 0.8f) - (speedUpgrade / 10f);
        StartCoroutine("ITapTapTimer", time);
        
        //print("Start , " + time);
    }

    private void OnDisable()
    {
        isTapTap_MG_Active = false;
    }

    public IEnumerator ITapTapTimer( float timeInterval)
    {
            while (true)
            {
                if (circleMain_ScrollBar.value < 1f && isTapTap_MG_Active)
                {
                    float rndCount = Random.Range(0.025f, 0.1f);
            
                    if (circleMain_ScrollBar.value + rndCount < 1f)
                    {
                        circleMain_ScrollBar.value += rndCount;
                    }
                    else
                    {
                        circleMain_ScrollBar.value += 1f - circleMain_ScrollBar.value;
                    }

                    if (circleMain_ScrollBar.value > 0.8f)
                    {
                        StopCoroutine("ITapTapTimer");
                        GameManager.Instance.Die();
                    }
                }

                yield return new WaitForSeconds(timeInterval);
            }
    }
}
