using UnityEngine;

public class MiniGame_Casino_System : MonoBehaviour
{
    public Transform[] driftPointsEnter;
    public Transform[] driftPointsExit;
    private short speed = 4;
    
    private GameObject getPlayer;
    
    private short nowFollowEnter = 0;
    private short nowFollowExit = 0;

    [SerializeField]private bool isEnter;
    
    public bool isActive;

    private void Update()
    {
        if (isActive && getPlayer != null)
        {
            if (nowFollowEnter != driftPointsEnter.Length && isEnter)
            {
                if (getPlayer.transform.position != driftPointsEnter[nowFollowEnter].position)
                {
                    getPlayer.transform.position = Vector3.MoveTowards(getPlayer.transform.position,
                        driftPointsEnter[nowFollowEnter].position, Time.deltaTime * speed * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));
                
                    getPlayer.transform.rotation = Quaternion.RotateTowards(getPlayer.transform.rotation, driftPointsEnter[nowFollowEnter].rotation, Time.deltaTime * (speed * 8) * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));
                }
                else
                    nowFollowEnter++;
            }
            else if (nowFollowExit != driftPointsExit.Length && GameManager.Instance.isCasinoExit)
            {
                if (getPlayer.transform.position != driftPointsExit[nowFollowExit].position)
                {
                    getPlayer.transform.position = Vector3.MoveTowards(getPlayer.transform.position,
                        driftPointsExit[nowFollowExit].position, Time.deltaTime * speed * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));
                
                    getPlayer.transform.rotation = Quaternion.RotateTowards(getPlayer.transform.rotation, driftPointsExit[nowFollowExit].rotation, Time.deltaTime * (speed * 10) * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));
                }
                else
                    nowFollowExit++;
            }
        }
    }

    public void SetPlayer(GameObject player)
    {
        isEnter = false;
        GameManager.Instance.isCasinoExit = false;
        
        nowFollowEnter = 0;
        nowFollowExit = 0;
        
        getPlayer = player;
        isEnter = true;
    }
}
