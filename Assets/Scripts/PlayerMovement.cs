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
    
    private bool grounded;
    private Rigidbody rigitbody;
    private bool gr;

    void Start()
    {
        rigitbody = GetComponent<Rigidbody>();
        rigitbody.freezeRotation = true;
    }
    
    private void Update()
    {
        gr = grounded;
        grounded = Physics.Raycast(transform.position, Vector3.down,0.25f,groundLayerMask);
        
        if (gr != grounded)
        {
            Debug.Log(grounded);
        }
        
        SetMovementSpeed();
        SpeedControl();
        SetGroundDrag();
    }

    public bool IsPlayerGrounded()
    {
        return grounded;
    }
    
    private void SetMovementSpeed()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= 2;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed /= 2;
    }

    private void SetGroundDrag()
    {
        if (grounded)
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

        if (grounded)
            rigitbody.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);

        else if(!grounded)
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
