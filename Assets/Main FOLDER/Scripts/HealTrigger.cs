using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    public GameObject mainObject;
    
    public AudioClip coinSound;

    private short randChance;
    
    private bool isActive;
    
    private void OnEnable()
    {
        isActive = checkSpawn();
        
        mainObject.SetActive(isActive);
    }
    
    private bool checkSpawn()
    {
        randChance = (short) Random.Range(0, 20);
        
        return (randChance <= 1 && GameManager.Instance.playerHealth < 3) ? true : false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            SoundManager.Instance.PlayOneShot(coinSound);
            GameManager.Instance.Heal();
            UIManager.Instance.SetScore(50);
            mainObject.SetActive(false);
        }
    }
}
