using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    //varaibles

    //config
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = .5f;
    [SerializeField] float spawnRandomizer = .3f;
    [SerializeField] int numOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getEnemyPrefab()
    {   return enemyPrefab;    }

    public GameObject getPathPrefab()
    {   return pathPrefab;    }

    public float getTimeBetweenSpawn()
    {   return timeBetweenSpawn;   }

    public int getNumOfEnemies()
    { return numOfEnemies; }

    public float getMoveSpeed()
    { return moveSpeed; }

    public float getSpawnRandomizer()
    { return spawnRandomizer; }
}
