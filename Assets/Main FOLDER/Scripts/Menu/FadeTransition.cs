using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void StartFade(float speedAnimation)
    {
        animator.speed = speedAnimation;
        animator.SetTrigger("isFade");
    }
}
