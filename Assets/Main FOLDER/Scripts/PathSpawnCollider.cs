using UnityEngine;
using Lean.Pool;
using Random = UnityEngine.Random;

public class PathSpawnCollider : MonoBehaviour {

    public float positionY = 0.81f;
    public Transform[] PathSpawnPoints;

    void OnTriggerEnter(Collider hit)
    {
        //player has hit the collider
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            int rand;
            if (!RoadSpawnSystem.Instance.onlyForward)
            {
                rand  = Random.Range(0, PathSpawnPoints.Length);
            }
            else
            {
                rand = 1;
            }
            
            //Left Road
            if (rand == 0)
            {
                ChooseRoadSpawn(RoadSpawnSystem.Instance.roadLeftPrefab, PathSpawnPoints[0]);
            }
            
            //Forward Road
            else if (rand == 1)
            {
                int randForward;
                randForward = Random.Range(0, 18);

                if (randForward == 0)
                {
                    ChooseRoadSpawn(RoadSpawnSystem.Instance.roadMiniGame, PathSpawnPoints[1]);
                }
                else
                {
                    ChooseRoadSpawn(RoadSpawnSystem.Instance.roadPrefab, PathSpawnPoints[1]); 
                }
            }
            
            //Right Road
            else if (rand == 2)
            {
                ChooseRoadSpawn(RoadSpawnSystem.Instance.roadRightPrefab, PathSpawnPoints[2]);
            }
        }
    }

    private void ChooseRoadSpawn(GameObject[] roadSide, Transform pathPoints)
    {
        GameObject road = LeanPool.Spawn(roadSide[Random.Range(0, roadSide.Length)], pathPoints.position, pathPoints.rotation);

        Vector3 rotation = pathPoints.rotation.eulerAngles;
        rotation.y += 90;
        Vector3 position = pathPoints.position;
        position.y += positionY;
        
        //LeanPool.Despawn(road, 10f - RoadSpawnSystem.Instance.timerSpeedDestroy);
    }
}
