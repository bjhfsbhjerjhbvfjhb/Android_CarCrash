using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnBoxesController : MonoBehaviour
{
    [Header("Box Create")]
    public GameObject mainBoxObject;
    public Transform spawnBoxTransform;
    private GameObject nowBox;
    
    public ParticleSystem boxExplosionEffect;
    public Animator hummerBoxAnimator;
    
    [Header("Reward")]
    public GameObject[] rewardObjects;
    public Animator rewardPanelAnimator;
    public int countKeys;
    public TextMeshPro countKeys_TMP;

    public Text toDoText, backRewardText_Header, backRewardText_Under, rewardText_Main;

    [Header("Bool")]
    public bool canUseHammer;
    public bool isWait;

    private void Start()
    {
        nowBox = Instantiate(mainBoxObject, spawnBoxTransform);
        countKeys_TMP.text = "X" + countKeys;
    }

    public void SpawnNewBox()
    {
        nowBox = Instantiate(mainBoxObject, spawnBoxTransform);
        canUseHammer = false;
        isWait = false;
    }

    public void Destruct()
    {
        if (nowBox)
        {
            nowBox.GetComponent<DestructibleBox>().TakeDamageDestroy();
            boxExplosionEffect.Play();

            int randReward = Random.Range(0, rewardObjects.Length);
            switch (randReward)
            {
                case 0:
                    rewardObjects[randReward].SetActive(true);
                    SetRewardText("НАГРАДА- КЛЮЧ");
                    break;
                case 1:
                    rewardObjects[randReward].SetActive(true);
                    SetRewardText("НАГРАДА- ВАЛЮТА");
                    break;
            }
            rewardPanelAnimator.SetTrigger("isTrigger");
            Invoke("DeactivateAllRewardObject", 2.5f);
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && nowBox)
        {
            if (!canUseHammer && countKeys > 0)
            {
                nowBox.GetComponent<Animator>().SetTrigger("openKey");
                toDoText.text = "Нажмите на экран чтобы разбить";
                
                countKeys--;
                countKeys_TMP.text = "X" + countKeys;
                canUseHammer = true;
            }
            else
            if (canUseHammer && !isWait && countKeys >= 0)
            {
                int randAnim = Random.Range(0, 2);
                if (randAnim == 0)
                    hummerBoxAnimator.SetTrigger("toDamage");
                else hummerBoxAnimator.SetTrigger("isDamage2");

                isWait = true;
                toDoText.text = "Нажмите на экран чтобы открыть";
                Invoke("Destruct", 0.4f);
                Invoke("SpawnNewBox", 2f);
            }
        }
    }

    public void DeactivateAllRewardObject()
    {
        for (int i = 0; i < rewardObjects.Length; i++)
        {
            rewardObjects[i].SetActive(false);
        }
    }

    public void SetRewardText(string text)
    {
        rewardText_Main.text = text;
        backRewardText_Under.text = text;
        backRewardText_Header.text = text;
    }

    public void BackMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
