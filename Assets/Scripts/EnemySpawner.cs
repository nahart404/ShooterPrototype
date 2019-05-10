using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //config
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    //variables
    int startingWave = 0; //start of the List

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); //start coroutine
        }
        while (looping); //while looping = true
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];

            yield return StartCoroutine(SpawnEnemyWave(currentWave));
        }
    }
    private IEnumerator SpawnEnemyWave(WaveConfig waveConfig)
    {
        int enemyCount = waveConfig.GetNumOfEnemies();
        for (int i = 0; i < enemyCount; i++)
        {
            //store and create new enemy on the scene
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWayPoints()[0].transform.position,
                        Quaternion.identity);

            //Sets the new enemy's wave Config
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    
    }
}
