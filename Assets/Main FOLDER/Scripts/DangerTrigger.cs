using UnityEngine;

public class DangerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Damage(1);
            UIManager.Instance.SetScore(-500);
        }
    }
}
