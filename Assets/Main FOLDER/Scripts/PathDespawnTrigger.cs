using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PathDespawnTrigger : MonoBehaviour
{
    public GameObject despawnObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeanPool.Despawn(despawnObject, 0.5f);
        }
    }
}
