using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCollision : MonoBehaviour
{
    [SerializeField] float delayDestroy = 5f;
     void OnCollisionEnter2D(Collision2D other) 
    {
        
        Invoke(nameof(DelayDestroyObject), delayDestroy);
        
    }

    void DelayDestroyObject()
    {
        Destroy(gameObject);
    }

}
