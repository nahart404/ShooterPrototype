using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables
    [Header("Player")] //creates a header in Unity incase we need to express what serialized fields do what
    float moveSpeed = 10f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float shipPadding = 1f;
    [SerializeField] int health = 200;

    [Header("Player Lasers")]
    float laserSpeed = 20f;
    float projectileFirePeriod = .1f;

    Coroutine firingCoroutine;

    //config
    [SerializeField] GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundaries();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    //player can fire projectiles
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) //using ButtonDown instead of keydown b/c it refers to the user's input manager, making easier for the user to reconfig controls
        {//note: input manager for fire1 was changed to "space"

            //make it to where holding down space fires instead of constantly having to press/click
            firingCoroutine = StartCoroutine(ContinuousFire());

            //problem: getbuttondown only returns 1 true when the button is held down
            //solution: add a while loop to the coroutine of "while true" and another if statement here 
            //for when player releases button and stopping that coroutine
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ContinuousFire()
    {
        while (true)
        {
            GameObject laser = Instantiate(projectilePrefab, transform.position,
                Quaternion.identity) as GameObject;
            //Quaternion.identity = "no rotation"
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            yield return new WaitForSeconds(projectileFirePeriod);
        }
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

    private void OnTriggerEnter2D(Collider2D other) //thing that bummed into enemy
    {
        //set dmgDealer to equal the laser (aka the "other")
        DamageDealer dmgDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!dmgDealer) { return; } //incase something unexpected happened
        DealDamage(dmgDealer);
    }

    private void DealDamage(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage(); //when hit,take away the health of the enemy
        dmgDealer.Hit(); //makes sure laser destroys itself

        //if 0 health,
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
