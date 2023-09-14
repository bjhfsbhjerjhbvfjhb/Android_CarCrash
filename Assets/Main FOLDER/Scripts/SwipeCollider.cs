using UnityEngine;
using System.Collections;

public class SwipeCollider : MonoBehaviour
{
    // Use this for initialization
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            GameManager.Instance.CanSwipe = true;
            hit.GetComponent<PlayerMove>().animator.SetBool("isTapDrift", false);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
            GameManager.Instance.CanSwipe = false;
    }
}
