using System;
using UnityEngine;

public class MiniGame_Casino_Trigger : MonoBehaviour
{
    public enum TypeTrigger
    {
        Enter,
        Casino,
        Exit
    };

    private GameManager gameManager;
    
    public TypeTrigger type;

    public MiniGame_Casino_System MG_Casino_System;

    private PlayerMove pm;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pm == null)
            {
                pm = other.GetComponent<PlayerMove>();
            }
            
            if (type == TypeTrigger.Enter)
            {
                MG_Casino_System.SetPlayer(other.gameObject);
                MG_Casino_System.isActive = true;
                pm.enabled = false;
                pm.TurnOffDrift();
                pm.animator.SetBool("isMiniGameDrift",true);
                RoadSpawnSystem.Instance.OnForwardRoadTime();
            }
            else if (type == TypeTrigger.Casino)
            {
                pm.animator.SetBool("isMiniGameDrift",false);
                gameManager.isCasinoExit = false;
                gameManager.CasinoMiniGame_Panel.SetActive(true);
            }
            else if (type == TypeTrigger.Exit)
            {
                pm.enabled = true;
                pm.animator.SetBool("isMiniGameDrift",false);
                MG_Casino_System.isActive = false;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pm == null)
            {
                pm = other.GetComponent<PlayerMove>();
            }
            
            if (type == TypeTrigger.Enter)
            {
                
            }
            else if (type == TypeTrigger.Casino)
            {
                other.GetComponent<PlayerMove>().animator.SetBool("isMiniGameDrift",true);
            }
            else if (type == TypeTrigger.Exit)
            {
                
            }
        }
    }
}
