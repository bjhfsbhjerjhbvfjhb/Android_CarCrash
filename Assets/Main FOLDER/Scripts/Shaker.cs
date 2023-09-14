using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shaker : MonoBehaviour
{
    [Range(0, 2f)]
    public float intensity;
    
    private Transform target;
    private Vector3 initialPos;
    private Vector3 initialRot;

    private float pendingShakeDuration = 0f;
    
    private bool isShaking = false;
    private void Start()
    {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
        initialRot = target.localEulerAngles;
    }

    private void Update()
    {
        if (pendingShakeDuration > 0 && !isShaking)
        {
            StartCoroutine(DoShake());
        } 
    }

    public void Shake(float duration)
    {
        if (duration > 0)
        {
            pendingShakeDuration += duration;
        }
    }

    private IEnumerator DoShake()
    {
        isShaking = true;

        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + pendingShakeDuration)
        {
            Vector3 randomPoint = new Vector3(Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity);
            target.localPosition += randomPoint;
            
            Vector3 randomEuler = new Vector3(Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity);
            target.localEulerAngles += randomEuler;
            
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = initialPos;
        target.localEulerAngles = initialRot;
        isShaking = false;
    }
}
