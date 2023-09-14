using UnityEngine;
using System.Collections;
using Lean.Pool;

public class TimeDestroyer : MonoBehaviour
{
    public float LifeTime = 10f;

    // Use this for initialization
    void Start()
    {
        if (GameManager.Instance.GameState == GameState.Playing)
            LeanPool.Despawn(gameObject, LifeTime);
    }
}
