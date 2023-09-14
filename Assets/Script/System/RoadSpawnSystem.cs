using System.Collections;
using UnityEngine;

public class RoadSpawnSystem : MonoBehaviour
{
    public GameObject[] roadPrefab;
    public GameObject[] roadLeftPrefab;
    public GameObject[] roadRightPrefab;
    public GameObject[] roadMiniGame;

    public bool onlyForward;
    
    private static RoadSpawnSystem instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    
    public static RoadSpawnSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RoadSpawnSystem();
            }
            return instance;
        }
    }

    public void OnForwardRoadTime()
    {
        if (!onlyForward)
        {
            StartCoroutine("ForwardRoadEvent");
        }
    }

    IEnumerator ForwardRoadEvent()
    {
        onlyForward = true;
        yield return new WaitForSeconds(2f);
        onlyForward = false;
    }
}
