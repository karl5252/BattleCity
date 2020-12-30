using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl_Wave : SpawnManager_Basic
{
    public int totalWaves;
    public float waveTimer;
    public int totalEnemyPerWave;

    private int enemyCount;
    private float waveInterval;
    private int numberOfWaves;
    private bool waveSpawned;

    public override void Spawn()
    {
        if (numberOfWaves <= totalWaves)
        {
            waveInterval += Time.deltaTime;

            Spawn();
            enemyCount++;

            // checks if the time is equal to the time required for a new wave
            if (waveInterval >= waveTimer)
            {
                waveSpawned = true;
                waveInterval = 0.0f;
                numberOfWaves++;
                enemyCount = 0;
            }
            if (enemyCount >= totalEnemyPerWave)
            {
                // diables the wave spawner
                waveSpawned = false;
            }
        }

    }
    
}
