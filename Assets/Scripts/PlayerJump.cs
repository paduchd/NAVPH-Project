using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private PlayerMovement playerMovement;
    
    private bool readyToJump;
    private new Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        readyToJump = true;
    }
    
    private void Update()
    {
        Jump();
    }
    
    private void Jump()
    {
        if (Input.GetKey(jumpKey) && readyToJump && playerMovement.IsPlayerGrounded())
        {
            readyToJump = false;

            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
