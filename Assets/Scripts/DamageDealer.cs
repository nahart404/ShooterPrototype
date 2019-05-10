using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100; //amount of damage

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        //when hit, destroy gameObject
        Destroy(gameObject);
    }
}
