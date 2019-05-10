using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
