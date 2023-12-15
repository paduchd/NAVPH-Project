using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement attributes")]
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float runningSpeedMultiplier = 1.5f;
    [SerializeField] private float groundDrag = 1f;
    [SerializeField] private float airMultiplier = 1f;
    
    [Header("Script imports")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private MovementAnimations movementAnimator;
    
    private Rigidbody rigitbody;
    private MovementState playerMovementState;
    private bool isGrounded;
    private bool speedBoosted = false;
    
    public enum MovementState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
    }
    
    void Start()
    {
        rigitbody = GetComponent<Rigidbody>();
        rigitbody.freezeRotation = true;
        playerMovementState = MovementState.Idle;
    }
    
    private void Update()
    {   //set ground state
        isGrounded = Physics.Raycast(transform.position, Vector3.down,0.25f,groundLayerMask);
        
        SetMovementState();
        SpeedControl();
        SetGroundDrag();
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    public MovementState GetMovementState()
    {
        return playerMovementState;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    
    private void SetMovementState()
    {
        bool isStationary = Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0;
        bool runOnCooldown = playerStamina.CanRun();
        
        //running
        if (!isStationary && Input.GetKey(KeyCode.LeftShift) && !runOnCooldown)
        {
            if (!speedBoosted)
            {
                movementSpeed *= runningSpeedMultiplier;
                speedBoosted = true;
            }
            playerMovementState = MovementState.Running;
            movementAnimator.AnimateRunning();
            PlayerEventManager.TriggerOnMovement();
            
        //walking
        } else if (!isStationary)
        {
            if (speedBoosted)
            {
                movementSpeed /= runningSpeedMultiplier;
                speedBoosted = false;
            }
            playerMovementState = MovementState.Walking;
            movementAnimator.AnimateWalking();
            PlayerEventManager.TriggerOnMovement();
        }
        //idle
        else
        {
            playerMovementState = MovementState.Idle;
            movementAnimator.AnimateIdle();
            PlayerEventManager.TriggerOnMovementStop();
        }
        
    }

    private void SetGroundDrag()
    {
        if (isGrounded)
            rigitbody.drag = groundDrag;
        else
            rigitbody.drag = 0;
    }
    
    private Vector3 GetMovementDirection()
    {
        return orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
    }
    
    private void MovePlayer()
    {
        Vector3 moveDirection = GetMovementDirection();

        if (isGrounded)
            rigitbody.AddForce(moveDirection.normalized * (movementSpeed * 10f), ForceMode.Force);
        else
            rigitbody.AddForce(moveDirection.normalized * (movementSpeed * 10f * airMultiplier), ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigitbody.velocity.x, 0f, rigitbody.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rigitbody.velocity = new Vector3(limitedVel.x, rigitbody.velocity.y, limitedVel.z);
        }
    }
    
}
