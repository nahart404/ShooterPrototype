using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //method that helps move the player via key commands (wasd)
    private void Move()
    {
        //horizonal movement
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; 
        //Time.deltaTime makes it to where movement 
        //doesn't depend on a player's framerate (faster fro faster framerates, slower for less frames)

        var newXPos = transform.position.x + deltaX; //player's soon to be new x position + delta(change)

        //vertical movement
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos); //set new position

    }
}
