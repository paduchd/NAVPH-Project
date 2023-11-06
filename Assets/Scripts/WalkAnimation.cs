using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator movementAnimator;
    bool walking = false;
    bool running = false;
    bool idle = true;
    
    void Start()
    {
        movementAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftShift))
        {
            walking = true;
            running = false;
            idle = false;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            walking = false;
            running = true;
            idle = false;
        }
        else
        {
            walking = false;
            running = false;
            idle = true;
        }

        movementAnimator.SetBool("walking", walking);
        movementAnimator.SetBool("running", running);
        movementAnimator.SetBool("idle", idle);
        
        // if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     movementAnimator.SetBool("walking", true);
        // }
        // else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        // {
        //     movementAnimator.SetBool("walking", true);
        // }
        // else if (Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     movementAnimator.SetBool("running", true);
        // }
        // else if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     movementAnimator.SetBool("running", true);
        // }
        // else if (Input.GetKeyUp(KeyCode.LeftShift))
        // {
        //     movementAnimator.SetBool("running", false);
        // }
    }
}