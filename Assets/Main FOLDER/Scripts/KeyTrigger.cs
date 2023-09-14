using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public GameObject mainObject;
    
    public AudioClip coinSound;

    public bool notDespawn;
    
    private short randChance;
    private short rand;
    
    private void OnEnable()
    {
        if (!notDespawn)
        {
            randChance = (short)Random.Range(0, 30);

            if (randChance == 1)
            {
                mainObject.SetActive(true);
                rand = (short)Random.Range(100, 200);
            }
            else
            {
                mainObject.SetActive(false);
            }  
        }
        else
        {
            mainObject.SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (randChance == 1 || notDespawn))
        {
            SoundManager.Instance.PlayOneShot(coinSound);
            UIManager.Instance.SetScore(rand);
            mainObject.SetActive(false);
        }
    }
}
