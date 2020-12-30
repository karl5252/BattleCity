using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl_RandomLocation : SpawnManager_Basic
{
    //public GameObject enemyPrefabs = this.enemyPrefabs;
    public int[] dimensionsX;
    public int[] dimensionsY;
    public int spawnHeight = 2;
    public float spawnInterval = 6f;            // How long between each spawn.

    private Vector3 randSpawnPosition;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnInterval, spawnInterval);
    }
    public override void Spawn()
    {
        randSpawnPosition.x = Random.Range(dimensionsX[0], dimensionsX[1]);
        randSpawnPosition.y = spawnPoint.position.y + spawnHeight;
        randSpawnPosition.z = Random.Range(dimensionsX[0], dimensionsX[1]);
        
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)],
            spawnPoint.position, spawnPoint.rotation);
    }
}
