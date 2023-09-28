using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallHandler : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Rigidbody2D pivot;
    [SerializeField] float respawnDelay = 1f; 
    [SerializeField] float detachDelay = .12f;
     Camera mainCamera;
     Rigidbody2D currentBallRigidBody;  
     SpringJoint2D currentBallSprintJoint; 

    
     bool isDragging;
    
    void Start()
    {
        mainCamera = Camera.main; // we get the main camera to put in the variable mainCamera
        SpawnNewBall(); // when the game starts we want to spawn a new player or we throw a player
        
    }

  
    void Update()
    {

       if(currentBallRigidBody == null) { return ;} // To C to D look bottom notes
                                                    // since we dont have rigidbody now that sets to null from the bottom we will return 
                                                    // the code and do nothing. to avoid error.

       if(!Touchscreen.current.primaryTouch.press.isPressed) // this is the code if we are not touching the screen. if not we skip.
       {
           if(isDragging) // To A to B  // since we are isDragging is set to True; we will run this code block
           {
                LaunchBall(); // we run this method. to launch the player or ball
           }

           isDragging = false; // we put here since if we release dragging the kinematic is set to false.

           return; // and if not touching it will return the method. without continouing the code method update
       }

       isDragging = true; // From A to B // we are setting is Dragging to true.

       currentBallRigidBody.isKinematic = true; // we put to kinematic without the physics to aim the ball. if touching the screen.

       Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue(); // the touch position in vector 2 screen.

       Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition); // ScreenToworldPoint transform a point from screen space to world space
                                                                            //  we also get vector 3 since we are using 2D we have only Vector2 
       
       currentBallRigidBody.position = worldPosition; // this is where when we click/touch in the screen the player appears

        
       
    }
    
    void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(playerPrefab, pivot.position, Quaternion.identity);

        // playerPrefab - the prefab that we want to respawn
        // pivot.positon - the position we want to respawn so we put over the pivot.position
        // Quaternion.Identity - means Default rotation.
        // GameObject ballInstance = means Instantiate returns a gameobject. so we declared another variable.

        currentBallRigidBody = ballInstance.GetComponent<Rigidbody2D>(); // get the component for instance
        currentBallSprintJoint = ballInstance.GetComponent<SpringJoint2D>(); // get the component for instance

        currentBallSprintJoint.connectedBody = pivot; // we connectBody of spring joint to pivot
        // .connectedBody is a component of Springjoint2d. to connect to something for example the pivot. 
    }

    void LaunchBall() 
    {
        currentBallRigidBody.isKinematic = false; // we put back to dynamic when not touching the screen and set the ball player to physics again
        currentBallRigidBody = null; // we dont want our ball to snap back when we touch the screen so we want to forget it. 
        // From C to D

        Invoke(nameof(DetachTheBall), detachDelay); // we use invoke to delay the disabling of our springjoint 2d in matter of second.

        
    }

    void DetachTheBall()
    {
        currentBallSprintJoint.enabled = false; // we turn off or disabled the spring joint2d it wont hold the ball anymore.
        currentBallSprintJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay); // when we detach the ball we respawn a new one,


    }
    
   
}
