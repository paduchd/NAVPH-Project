using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Paramaters")] 
    private float currentStamina = 100f;
    [SerializeField] private float maxStamina = 100f;

    [Header("Stamina Regen & Drain Parameters")] 
    [Range(0, 50)] [SerializeField] private float runningCost = 10f;
    [SerializeField] private float runCooldown = 1f;
    [Range(0, 50)] [SerializeField] private float staminaRegenRate = 10f;
    [SerializeField] private float jumpCost = 20;
    
    [Header("Stamina Bar UI Elements")] 
    [SerializeField] private Image staminaBarSlider;
    [SerializeField] private Image staminaBarBackground;
    //[SerializeField] private CanvasGroup staminaBarCanvasGroup = null;

    [Header("Player movement")] 
    [SerializeField] private PlayerMovement playerMovement;

    private bool runOnCooldown;

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBarBackground.rectTransform.sizeDelta = new Vector2((458 * maxStamina) / 100 , staminaBarBackground.rectTransform.sizeDelta.y);
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
        //update new max staminabar width
        float newMaxStamina = maxStamina + amount;
        staminaBarBackground.rectTransform.sizeDelta = new Vector2((458 * newMaxStamina) / 100 , staminaBarBackground.rectTransform.sizeDelta.y);
        
        maxStamina += amount;
        UpdateSliderUI();
    }

    private void Regen()
    {
        if (currentStamina <= maxStamina - 0.01)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            UpdateSliderUI();
        }
    }
    
    //running
    private void RunningDrain()
    {
        currentStamina -= runningCost * Time.deltaTime;
        UpdateSliderUI();
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
    
    //jumping
    public void JumpDrain()
    {
        currentStamina -= jumpCost;
        UpdateSliderUI();
    }
    
    public bool CanJump()
    {
        if (jumpCost <= currentStamina)
        {
            return true;
        }
        return false;
    }
    
    //UI
    private void UpdateSliderUI()
    {
        staminaBarSlider.fillAmount = currentStamina / maxStamina;
    }

   
}
