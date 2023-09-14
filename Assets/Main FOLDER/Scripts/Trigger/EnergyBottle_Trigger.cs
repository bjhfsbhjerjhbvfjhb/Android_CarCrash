using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBottle_Trigger : MonoBehaviour
{
    public GameObject mainObject;
    public GameObject mainPS;
    
    public AudioClip energySound;
    
    private short randChance;

    private bool isActive;

    private void OnEnable()
    {
        isActive = checkSpawn();
        
        mainObject.SetActive(isActive);
        mainPS.SetActive(isActive);
    }

    private bool checkSpawn()
    {
        randChance = (short) Random.Range(0, 4);
        
        return (randChance <= 1 && GameManager.Instance.playerEnergy < 4) ? true : false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            SoundManager.Instance.PlayOneShot(energySound);
            UIManager.Instance.SetScore(50);
            GameManager.Instance.EnergyUP();
            mainObject.SetActive(false);
            mainPS.SetActive(false);
        }
    }
}
