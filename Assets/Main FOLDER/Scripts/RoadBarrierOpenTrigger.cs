using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadBarrierOpenTrigger : MonoBehaviour
{
    private Animator animator;

    public GameObject mainObject;
    
    private short randChance;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        mainObject.SetActive(CheckRand(3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckRand(2)) animator.SetTrigger("isOpen");
        }
    }
    
    private bool CheckRand(short maxCount)
    {
        randChance = (short)Random.Range(0, maxCount);
        return randChance == 1 ? true : false;
    }
}
