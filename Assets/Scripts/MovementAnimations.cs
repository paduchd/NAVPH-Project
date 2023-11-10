using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    private Animator movementAnimator;
    private bool walking = false;
    private bool running = false;
    private bool idle = true;

    private string WALKING = "walking";
    private string RUNNING = "running";
    private string IDLE = "idle";
    
    void Start()
    {
        movementAnimator = GetComponent<Animator>();
    }

    public void AnimateIdle()
    {
        walking = false;
        running = false;
        idle = true;
        SetAnimations();
    }

    public void AnimateWalking()
    {
        walking = true;
        running = false;
        idle = false;
        SetAnimations();
    }

    public void AnimateRunning()
    {
        walking = false;
        running = true;
        idle = false;
        SetAnimations();
    }

    private void SetAnimations()
    {
        movementAnimator.SetBool(WALKING, walking);
        movementAnimator.SetBool(RUNNING, running);
        movementAnimator.SetBool(IDLE, idle);
    }
    
    void Update()
    {
        
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