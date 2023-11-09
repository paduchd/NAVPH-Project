using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] private Transform orientation;
    [SerializeField] private Stamina playerStamina;

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
            if(!isRunning)
            {
                isRunning = true;
                moveSpeed *= 2;
            }
            
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
                moveSpeed /= 2;
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
            rigitbody.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);

        else if(!isGrounded)
            rigitbody.AddForce(moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigitbody.velocity.x, 0f, rigitbody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigitbody.velocity = new Vector3(limitedVel.x, rigitbody.velocity.y, limitedVel.z);
        }
    }

   
}
