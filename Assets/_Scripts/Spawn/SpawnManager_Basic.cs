using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Basic : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    //public Transform[] spawnPoints;
    public Transform spawnPoint;

    public virtual void Spawn()
    {
       // currentSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)],
            spawnPoint.position , spawnPoint.rotation);
    }             
}
