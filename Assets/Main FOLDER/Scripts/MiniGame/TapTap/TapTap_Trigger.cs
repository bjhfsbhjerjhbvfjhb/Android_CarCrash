using UnityEngine;

public class TapTap_Trigger : MonoBehaviour
{
    public enum TypeTrigger
    {
        Enter,
        Exit
    };

    public TypeTrigger type;

    public Drift_TapTap_System driftSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == TypeTrigger.Enter)
            {
                driftSystem.SetPlayer(other.gameObject);
                other.GetComponent<PlayerMove>().enabled = false;
                other.GetComponent<PlayerMove>().TurnOffDrift();
                other.GetComponent<PlayerMove>().animator.SetBool("isMiniGameDrift",true);
                RoadSpawnSystem.Instance.OnForwardRoadTime();
                GameManager.Instance.TapTapMiniGame_Panel.SetActive(true);
            }

            if (type == TypeTrigger.Exit)
            {
                other.GetComponent<PlayerMove>().enabled = true;
                other.GetComponent<PlayerMove>().animator.SetBool("isMiniGameDrift",false);
                GameManager.Instance.TapTapMiniGame_Panel.SetActive(false);
            }
        }
    }
}
