using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private MovementAnimations movementAnimator;
    
    private bool readyToJump = true;
    private new Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        Jump();
    }
    
    private void Jump()
    {
        bool enoughStamina = playerStamina.CanJump();
        
        if (Input.GetKey(jumpKey) && readyToJump && playerMovement.IsGrounded() && enoughStamina)
        {
            movementAnimator.AnimateJump();
            
            readyToJump = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            
            playerStamina.JumpDrain();
            
            Invoke(nameof(ResetJumpCooldown), jumpCooldown);
        }
        
    }

    private void ResetJumpCooldown()
    {
        readyToJump = true;
    }
}
