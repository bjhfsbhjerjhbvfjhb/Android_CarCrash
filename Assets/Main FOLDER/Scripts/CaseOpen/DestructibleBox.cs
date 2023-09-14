using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleBox : MonoBehaviour
{
    public GameObject destroyedVersion;

    public void TakeDamageDestroy()
    {
        GameObject destroyObject = Instantiate(destroyedVersion, transform.position, transform.rotation);

        foreach (Transform child in destroyObject.transform)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(Random.Range(100f, 500f), destroyObject.transform.position, Random.Range(5f, 20f));
            }
        }
            
        Destroy(gameObject);
        Destroy(destroyObject, 2.6f);
    }
}
