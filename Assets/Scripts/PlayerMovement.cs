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
    [SerializeField] private Stamina playerStamina;
    [SerializeField] private MovementAnimations movementAnimator;

    private bool isGrounded = true;
    private bool isRunning = false;
    private Rigidbody rigitbody;
    private bool gr;
    
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    void Start()
    {
        rigitbody = GetComponent<Rigidbody>();
        rigitbody.freezeRotation = true;
    }
    
    private void Update()
    {
        gr = isGrounded;
        isGrounded = Physics.Raycast(transform.position, Vector3.down,0.25f,groundLayerMask);
        
        if (gr != isGrounded)
        {
            Debug.Log(isGrounded);
        }
        
        SetMovementSpeed();
        SpeedControl();
        SetGroundDrag();
    }
    
    private void SetMovementSpeed()
    {
        bool isStationary = Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0;

        bool runOnCooldown = playerStamina.CanRun();
        
        if (Input.GetKey(KeyCode.LeftShift) && !isStationary && !runOnCooldown)
        {
            if(!isRunning) //shift pressed and movement pressed first time
            {
                isRunning = true;
                movementSpeed *= runningSpeedMultiplier;
            }
            movementAnimator.AnimateRunning();
        }
        else
        {
            if (isRunning) //shift or movement unpressed or runOnCooldow for first time
            {
                isRunning = false;
                movementSpeed /= runningSpeedMultiplier;
            }
            if(isStationary)
            {
                movementAnimator.AnimateIdle();
            }
            else
            {
                movementAnimator.AnimateWalking();
            }
        }
    }

    private void SetGroundDrag()
    {
        if (isGrounded)
            rigitbody.drag = groundDrag;
        else
            rigitbody.drag = 0;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
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

        else if(!isGrounded)
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
