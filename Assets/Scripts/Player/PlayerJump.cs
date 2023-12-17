using System.Collections;
using UnityEngine;

// Racoons jump mechanic
public class PlayerJump : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;

    [Header("Components")]
    private PlayerMovement playerMovement;
    private PlayerStamina playerStamina;
    [SerializeField] private MovementAnimations movementAnimator;
    
    private bool readyToJump = true;
    private new Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStamina = GetComponent<PlayerStamina>();
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
            StartCoroutine(JumpStart());
        }
    }
    
    IEnumerator JumpStart()
    {
        readyToJump = false;
        movementAnimator.AnimateJump();
        
        // Force added for jump
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        playerStamina.DrainStamina(PlayerStamina.MovementType.Jump);
        
        yield return new WaitForSeconds(jumpCooldown);
        readyToJump = true;
        
    }
    
}
