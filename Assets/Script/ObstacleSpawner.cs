using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 1f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 3f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")] [SerializeField] float maxXDistance = 33f;
    [SerializeField] protected GameObject[] obstacleToSpawn;
    [SerializeField] protected float timerToDestroy;
    void Start()
    {
        this.StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawns);
            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y,
                transform.position.z);
            int randObjNum = (int)Random.Range(0, obstacleToSpawn.Length);
            GameObject newObject = Instantiate(obstacleToSpawn[randObjNum], positionOfSpawnedObject, Quaternion.identity);
            newObject.GetComponent<GenkidamaAction>();
            
            
            this.StartCoroutine(destroyAfter(newObject));
        }
    }

    private IEnumerator destroyAfter(GameObject newObject)
    {
        yield return new WaitForSeconds(timerToDestroy);
        Destroy(newObject);

    }

}
