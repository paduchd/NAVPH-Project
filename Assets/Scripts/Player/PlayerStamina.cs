using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Paramaters")] 
    public float currentStamina = 100f;
    [SerializeField] private float maxReplishableStamina = 100;
    public float maxStamina = 100f;
    
    [Header("Stamina Regen & Drain Parameters")] 
    [Range(0, 50)] [SerializeField] private float runningCost = 10f;
    
    [Range(0, 50)] [SerializeField] private float staminaRegenRate = 10f;
    [SerializeField] private float jumpCost = 20;
    [SerializeField] private float dashCost = 50;
    [SerializeField] private float stunCost = 50;
    [SerializeField] private float singleAttackCost = 30;
    [SerializeField] private float aoeAttackCost = 40;
    [SerializeField] private float runCooldown = 1f;
    
    [Header("Player movement")] 
    [SerializeField] private PlayerMovement playerMovement;

    private bool runOnCooldown;
    
    private void Start()
    {
        //initialize stamina and max replishable stamina
        currentStamina = maxStamina;
        if (maxReplishableStamina <= maxStamina)
        {
            maxReplishableStamina = maxStamina;
        }
        
    }

    private void Update()
    {
        if(playerMovement.GetMovementState() == PlayerMovement.MovementState.Running)
        {
            RunningDrain();
        }
        else
        {
            Regen();
        }
        
    }
    
    public void IncreaseMax(float amount)
    {
        if (maxReplishableStamina > maxStamina)
        {
            //determine max stamina so that it dont replish past max replishable stamina
            float newMaxStamina;
            if (maxStamina + amount >= maxReplishableStamina)
            {
                newMaxStamina = maxReplishableStamina;
            }
            else
            {
                newMaxStamina = maxStamina + amount;
            }
            maxStamina = newMaxStamina;
            
            PlayerEventManager.TriggerOnFoodEaten(true);
            PlayerEventManager.TriggerOnStaminaUpdate();
        }
        else
        {
            PlayerEventManager.TriggerOnFoodEaten(false);
        }
    }

    private void Regen()
    {
        if (currentStamina <= maxStamina - 0.01)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            PlayerEventManager.TriggerOnStaminaUpdate();
        }
    }
    
    //running
    private void RunningDrain()
    {
        currentStamina -= runningCost * Time.deltaTime;
        PlayerEventManager.TriggerOnStaminaUpdate();
    }
    
    public bool CanRun()
    {
        if (currentStamina <= 0 & !runOnCooldown)
        {
            runOnCooldown = true;
            Invoke(nameof(ResetRunCooldown), runCooldown);
            return true;
        }
        return runOnCooldown;
    }

    private void ResetRunCooldown()
    {
        runOnCooldown = false;
    }
    
    public bool CanJump()
    {
        return jumpCost <= currentStamina;
    }
    
    public bool CanDash()
    {
        return dashCost <= currentStamina;
    }
    
    public bool CanSingleAttack()
    {
        return singleAttackCost <= currentStamina;
    }
    
    public bool CanAoeAttack()
    {
        return aoeAttackCost <= currentStamina;
    }

    public bool CanStun()
    {
        return stunCost <= currentStamina;
    }
    
    public void DrainStamina(MovementType movementType)
    {
        float staminaToDrain;
        switch (movementType)
        {
            case MovementType.Jump:
                staminaToDrain = jumpCost;
                break;

            case MovementType.Dash:
                staminaToDrain = dashCost;
                break;

            case MovementType.AoeAttack:
                staminaToDrain = aoeAttackCost;
                break;
            
            case MovementType.SingleAttack:
                staminaToDrain = singleAttackCost;
                break;

            default:
                staminaToDrain = 0;
                break;
        }
        
        currentStamina -= staminaToDrain;
        PlayerEventManager.TriggerOnStaminaUpdate();
    }
    
    public enum MovementType
    {
        Jump = 0,
        Dash = 1,
        SingleAttack = 2,
        AoeAttack = 3,
    }
    

   
}
