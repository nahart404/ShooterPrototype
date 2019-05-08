using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //config
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 10f;

    //variables
    int wayPointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {//set starting position of ship
        transform.position = waypoints[wayPointIndex].transform.position; //should be the very first waypoint
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (wayPointIndex <= waypoints.Count - 1) //if we haven't gone to the next waypoint yet,
        {
            //move towards the current waypoint
            //first get the position we need to move towards
            var targetPosition = waypoints[wayPointIndex].transform.position;
            var movementPerFrame = moveSpeed * Time.deltaTime;

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
