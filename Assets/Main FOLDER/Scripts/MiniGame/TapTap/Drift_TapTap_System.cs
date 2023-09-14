using UnityEngine;

public class Drift_TapTap_System : MonoBehaviour
{
    public Transform[] driftPoints;
    private short speed = 6;
    
    private GameObject getPlayer;
    private short nowFollow = 0;

    private void Update()
    {
        if (getPlayer != null && nowFollow != driftPoints.Length)
        {
            if (getPlayer.transform.position != driftPoints[nowFollow].position)
            {
                getPlayer.transform.position = Vector3.MoveTowards(getPlayer.transform.position,
                    driftPoints[nowFollow].position, Time.deltaTime * speed * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));

                if (nowFollow != driftPoints.Length - 2)
                {
                    getPlayer.transform.rotation = Quaternion.RotateTowards(getPlayer.transform.rotation, driftPoints[nowFollow].rotation, Time.deltaTime * (speed + 20) * (getPlayer.GetComponent<PlayerMove>().speedUpgrade)); 
                }
                else
                {
                    getPlayer.transform.rotation = Quaternion.RotateTowards(getPlayer.transform.rotation, driftPoints[nowFollow].rotation, Time.deltaTime * (speed + 40) * (getPlayer.GetComponent<PlayerMove>().speedUpgrade));
                }
            }
            else
                nowFollow++;
        }
    }

    public void SetPlayer(GameObject player)
    {
        nowFollow = 0;
        getPlayer = player;
    }
}
