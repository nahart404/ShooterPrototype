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
    [SerializeField] float explodeDuration = 1f;

    //config
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explodeVFX;
    [SerializeField] AudioClip laserAudio;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] [Range(0, 2)] float deathAudioVolume = 2f;
    [SerializeField] float laserAudioVolume = .5f;

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
        //play laser audio clip at laser position (might be an issue, if something happens, look here. 11/6/19)
        //Instead, could use AudioSource.PlayClipAtPoint(laserAudio, Camera.main.transform.position, laserAudioVolume) for less "3D sounds from laser
        AudioSource.PlayClipAtPoint(laserAudio, new Vector3(laser.transform.position.x, laser.transform.position.y), laserAudioVolume);
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
            Die();
        }
    }

    //method for when the enemy dies(particle effects, deleting game objects)
    private void Die()
    {
        Destroy(gameObject);
        //Inst the particle effect for death
        GameObject explosion = Instantiate(explodeVFX, transform.position, transform.rotation);
        //then destory it (we only need it for a second)
        Destroy(explosion, explodeDuration);
        //play audio clip for death
        AudioSource.PlayClipAtPoint(deathAudio, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), deathAudioVolume);
    }
}
