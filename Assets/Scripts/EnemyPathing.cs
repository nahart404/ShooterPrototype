using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //config
    [SerializeField] List<Transform> waypoints;
    WaveConfig waveConfig;

    //variables
    int wayPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        //set waypoints of enemy
        waypoints = waveConfig.GetWayPoints();

        //set starting position of ship
        transform.position = waypoints[wayPointIndex].transform.position; //should be the very first waypoint
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    //method used to help set/change the wave/path of the ship to have variety
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        //this instance (waveConfig in the class) equals the waveConfig in this method
        this.waveConfig = waveConfig;
    }
    private void MoveEnemy()
    {
        if (wayPointIndex <= waypoints.Count - 1) //if we haven't gone to the next waypoint yet,
        {
            //move towards the current waypoint
            //first get the position we need to move towards
            var targetPosition = waypoints[wayPointIndex].transform.position;
            var movementPerFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            /*Cut out the extra variable "moveSpeed" and just referenced the class method that 
             * had the speed to begin with
             */

            //now the actual moving
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementPerFrame);

            if (transform.position == targetPosition) //if ship made it to target position,
            {
                //inc wayPointIndex
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
