using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwipeHintTrigger : MonoBehaviour
{
    public ParticleSystem whiteBoxPS;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Renderer>().material.SetColor("_MainColor",new Color(0f, 0.4f, 0.8f, 1));
            whiteBoxPS.Emit(1);
            //whiteBoxPS.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Renderer>().material.SetColor("_MainColor",new Color(0f, 0.9f, 0.1f, 1));
        }
    }
}
