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

    public GameObject GetEnemyPrefab()
    {   return enemyPrefab;    }

    //return list of waypoint transforms for each wave
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform) //for each "child" under Path game Object,
        {
            waveWayPoints.Add(child); //create new child for children of Path
        }

        return waveWayPoints; 
    }

    public float GetTimeBetweenSpawn()
    {   return timeBetweenSpawn;   }

    public int GetNumOfEnemies()
    { return numOfEnemies; }

    public float GetMoveSpeed()
    { return moveSpeed; }

    public float GetSpawnRandomizer()
    { return spawnRandomizer; }
}
