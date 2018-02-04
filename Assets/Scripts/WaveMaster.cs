using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaster : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveNumber = 1;

    private int waveIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
            countdown = timeBetweenWaves;
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.rotation);
    }

}
