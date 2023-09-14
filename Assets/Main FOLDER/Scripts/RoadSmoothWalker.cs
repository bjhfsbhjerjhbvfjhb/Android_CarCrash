using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSmoothWalker : MonoBehaviour
{
    public GameObject target;
    
    void LateUpdate ()
    {
        transform.position = new Vector3(target.transform.position.x, 0.5f, target.transform.position.z);
    }
}
