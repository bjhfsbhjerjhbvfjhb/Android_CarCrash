using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class TriggerRoadSystem : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public GameObject[] roadPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            GameObject road = LeanPool.Spawn(roadPrefab[0], endPoint.position, Quaternion.identity);
            road.transform.rotation = Quaternion.Euler(-90, 0 , 0);
            road.transform.position = new Vector3(road.transform.localPosition.x + 2, road.transform.localPosition.y, road.transform.localPosition.z);
        }
    }
}
