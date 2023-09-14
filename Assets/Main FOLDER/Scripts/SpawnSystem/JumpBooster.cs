using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public GameObject mainObject;
    
    private short randChance;
    private short rand;
    
    private void OnEnable()
    {
        randChance = (short)Random.Range(0, 8);

        if (randChance == 1)
        {
            mainObject.SetActive(true);
        }
        else
        {
            mainObject.SetActive(false);
        }  
    }
}
