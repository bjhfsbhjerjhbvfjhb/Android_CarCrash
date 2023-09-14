using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostTrigger : MonoBehaviour
{
    public Animator boostAnim;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().JumpBoosterStart();
            boostAnim.SetTrigger("isBoost");
            RoadSpawnSystem.Instance.OnForwardRoadTime();
        }
    }
}
