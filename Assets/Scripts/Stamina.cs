using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Paramaters")] [SerializeField]
    public float currentStamina = 100f;
    [SerializeField] private float maxStamina = 100f;

    [Header("Stamina Regen & Drain Parameters")] 
    [Range(0, 50)] [SerializeField] private float runningCost = 10f;
    [SerializeField] private float runCooldown = 1f;
    [Range(0, 50)] [SerializeField] private float staminaRegen = 10f;
    [SerializeField] private float jumpCost = 20;
    
    [Header("Stamina UI Elements")] 
    [SerializeField] private Image staminaProgressUI = null;
    //[SerializeField] private CanvasGroup staminaBarCanvasGroup = null;

    [Header("Player movement")] 
    [SerializeField] private PlayerMovement playerMovement;

    private bool runOnCooldown;
    
    private void Update()
    {
        if(playerMovement.GetMovementState() == PlayerMovement.MovementState.Running)
        {
            RunningDrain();
            
        }
        else
        {
            StaminaRegen();
        }
    }

    private void StaminaRegen()
    {
        if (currentStamina <= maxStamina - 0.01)
        {
            currentStamina += staminaRegen * Time.deltaTime;
            UpdateStaminaUI();
        }
    }

    private void RunningDrain()
    {
        currentStamina -= runningCost * Time.deltaTime;
        UpdateStaminaUI();
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
    
    public void JumpDrain()
    {
        currentStamina -= jumpCost;
        UpdateStaminaUI();
    }

    public bool CanJump()
    {
        if (jumpCost <= currentStamina)
        {
            return true;
        }
        return false;
    }

    
    private void UpdateStaminaUI()
    {
        staminaProgressUI.fillAmount = currentStamina / maxStamina;
    }
}
