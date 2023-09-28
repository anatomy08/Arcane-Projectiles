using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public GameObject deathEffect; // death effect clouds, put prefab here
    [SerializeField] float stageDelay = 2f;
    public float health = 4f; // health of enemies

    public static int enemiesAlive = 0;

    void Start()
    {
        enemiesAlive++; // The Amount of Enemies Appear in the scene, if 4 then enemies live is 4;
        
    }

    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.relativeVelocity.magnitude > health) // the amount of velocity impact to the Enemy if its > health then run Die() method
        {
            
            Die();
        }

    }

    void Die()
    {
         Instantiate(deathEffect, transform.position, Quaternion.identity); // we instance Effect from Prefab

        enemiesAlive--;
        Debug.Log("Total of enemies Remaining: " + enemiesAlive);
        
        if(enemiesAlive <= 0)
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        
    }

    IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(stageDelay);
        Debug.Log("LoadNext Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
