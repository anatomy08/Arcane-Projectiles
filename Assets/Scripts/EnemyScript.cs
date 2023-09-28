using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    public float health = 4f;
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.relativeVelocity.magnitude > health)
        {
            Die();
        }

    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
