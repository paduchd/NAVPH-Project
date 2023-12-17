using UnityEngine;
using System.Collections;

// Racoons dash mechanic
public class PlayerDash : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private KeyCode dashKey = KeyCode.LeftControl;
    [SerializeField] private Transform orientation;
    [SerializeField] private float dashForce = 20;
    [SerializeField] private float dashUpwardForce = 0;
    public static float dashCooldown = 3f;
    
    private PlayerStamina playerStamina;
    private PlayerMovement playerMovement;
    private Rigidbody rigidbody;

    private bool readyToDash = true;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerStamina = GetComponent<PlayerStamina>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        bool enoughStamina = playerStamina.CanDash();
        
        if (Input.GetKey(dashKey) && readyToDash && enoughStamina && playerMovement.IsGrounded())
        {
            StartCoroutine(DashStart());
        }
        
    }
    IEnumerator DashStart()
    {
        PlayerEventManager.TriggerOnDash();
        readyToDash = false; 
        
        Vector3 dashForceVector = -orientation.forward * dashForce + orientation.up * dashUpwardForce;
        rigidbody.AddForce(dashForceVector,ForceMode.Impulse);
        
        playerStamina.DrainStamina(PlayerStamina.MovementType.Dash);
        
        yield return new WaitForSeconds(dashCooldown);
        readyToDash = true;
        
    }
    
}
