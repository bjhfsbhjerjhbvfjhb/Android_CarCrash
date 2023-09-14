using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiniGame_Casino_UI : MonoBehaviour
{
    private Animator animator;

    public GameObject spinButton;
    public GameObject exitButton;

    public Sprite[] fruitsSprite;
    public Image[] endFruitImage;

    public Text statusText;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        statusText.enabled = false;
        spinButton.SetActive(true);
        exitButton.SetActive(false);
    }

    public void SpinButtonPress()
    {
        animator.speed = Random.Range(0.5f, 1f);
        animator.SetBool("isSpin", true);

        spinButton.SetActive(false);

        int randWind = Random.Range(0, 8);
        if (randWind == 0)
        {
            for (int i = 0; i < endFruitImage.Length; i++)
            {
                endFruitImage[i].sprite = fruitsSprite[0];
            }
        }
        else
        {
            for (int i = 0; i < endFruitImage.Length; i++)
            {
                endFruitImage[i].sprite = fruitsSprite[Random.Range(0, fruitsSprite.Length)];
            }
        }
    }

    public void ExitButtonPress()
    {
        animator.SetBool("isSpin", false);
        GameManager.Instance.CasinoMiniGame_Panel.SetActive(false);
        GameManager.Instance.isCasinoExit = true;
    }

    public void CheckWin()
    {
        exitButton.SetActive(true);
        statusText.enabled = true;
        
        if (endFruitImage[0].sprite == endFruitImage[1].sprite && endFruitImage[1].sprite == endFruitImage[2].sprite)
        {
            statusText.text = "Вы выиграли - " + Random.Range(200, 500) + "$";
            UIManager.Instance.SetScore(Random.Range(2000, 4000));
        }
        else
        {
            statusText.text = "Вы проиграли";
        }
    }
}
