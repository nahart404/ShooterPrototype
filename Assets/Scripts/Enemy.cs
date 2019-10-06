using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    float laserSpeed = 20f;

    //config
    [SerializeField] GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownShooter();
    }
    private void CountDownShooter()
    {
        shotCounter -= Time.deltaTime; //independent timer from the game timer
        //shotCounter = shotCounter - Time is what this is saying

        if (shotCounter <= 0f)
        {
            //shoot
            Fire(); //method to have enemy shoot
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            //resets Counter after every shot
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectilePrefab, transform.position,
                Quaternion.identity) as GameObject;
        //Quaternion.identity = "no rotation"
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) //thing that bummed into enemy
    {
        //set dmgDealer to equal the laser (aka the "other")
        DamageDealer dmgDealer = other.gameObject.GetComponent<DamageDealer>();
        DealDamage(dmgDealer);
    }

    private void DealDamage(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage(); //when hit,take away the health of the enemy

        //if 0 health,
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
