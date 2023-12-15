using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private KeyCode dashKey = KeyCode.LeftControl;
    [SerializeField] private Transform orientation;
    [SerializeField] private float dashForce = 20;
    [SerializeField] private float dashUpwardForce = 0;
    [SerializeField] private float dashCooldown = 0.25f;

    [Header("Components")]
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private MovementAnimations movementAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    private Rigidbody rigidbody;

    private bool readyToDash = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    private void Dash()
    {
        bool enoughStamina = playerStamina.CanDash();

        if (Input.GetKey(dashKey) && readyToDash && enoughStamina && playerMovement.IsGrounded())
        {
            PlayerEventManager.TriggerOnAoe();
            
            readyToDash = false; 
            Vector3 dashForceVector = -orientation.forward * dashForce + orientation.up * dashUpwardForce;
            rigidbody.AddForce(dashForceVector,ForceMode.Impulse);

            playerStamina.DrainStamina(PlayerStamina.MovementType.Dash);
        
            Invoke(nameof(ResetDash),dashCooldown);
        }
        
    }

    private void ResetDash()
    {
        readyToDash = true;
    }
}
