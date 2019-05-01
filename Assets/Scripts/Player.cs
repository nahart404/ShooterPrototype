using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    float moveSpeed = 10f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float shipPadding = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundaries();
    }

    //method to control where the player can and can't go using the camera's viewport
    private void SetMoveBoundaries()
    {
        Camera gameCamera = Camera.main; //main camera in the scene
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + shipPadding; //min of x axis
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - shipPadding; //max of x axis 

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + shipPadding; //min of y axis
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - shipPadding; //max of y axis 
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
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); //player's soon to be new x position + delta(change) with limits

        //vertical movement
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos); //set new position

    }
}
