using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //config
    [SerializeField] List<WaveConfig> waveConfigs;

    //variables
    int startingWave = 0; //start of the List

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWave]; //set up currentwave
        StartCoroutine(SpawnEnemyWave(currentWave)); //start coroutine
    }

    private IEnumerator SpawnEnemyWave(WaveConfig waveConfig)
    {
        int enemyCount = waveConfig.GetNumOfEnemies();
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWayPoints()[0].transform.position,
                        Quaternion.identity);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    
    }
}
