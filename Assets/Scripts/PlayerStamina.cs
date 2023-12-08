using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Paramaters")] 
    private float currentStamina = 100f;
    [SerializeField] private float maxReplishableStamina = 100;
    [SerializeField] private float maxStamina = 100f;
    
    [Header("Stamina Regen & Drain Parameters")] 
    [Range(0, 50)] [SerializeField] private float runningCost = 10f;
    [SerializeField] private float runCooldown = 1f;
    [Range(0, 50)] [SerializeField] private float staminaRegenRate = 10f;
    [SerializeField] private float jumpCost = 20;
    [SerializeField] private float dashCost = 50;
    
    [Header("Stamina Bar UI Elements")] 
    [SerializeField] private Image staminaBarSlider;
    [SerializeField] private Image staminaBarBackground;
    
    [Header("Stamina Overlay")] 
    [SerializeField] private Image staminaOverlay;
    [SerializeField] private float overlayFadeTime = 2.0f; //time in s it takes for the green overlay to fade out
    private float currentFadeTime;
    private bool overlayIsFading;

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
        
        //initialize stamina bar width based on max stamina
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

        if (overlayIsFading)
        {
            StaminaOverlayFading();
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
            
            //update stamina bar width to new max
            staminaBarBackground.rectTransform.sizeDelta = new Vector2((458 * newMaxStamina) / 100 , staminaBarBackground.rectTransform.sizeDelta.y);
        
            //show stamina overlay
            overlayIsFading = true;
            currentFadeTime = 0.0f;
            staminaOverlay.gameObject.SetActive(true);
        
            //update max stamina and stamina bar
            maxStamina = newMaxStamina;
            UpdateSliderUI();
        }
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

    //dashing
    public void DashDrain()
    {
        currentStamina -= dashCost;
        UpdateSliderUI();
    }

    public bool CanDash()
    {
        if(dashCost <= currentStamina)
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
    
    private void StaminaOverlayFading()
    {
        currentFadeTime += Time.deltaTime;

        // Calculate the alpha value based on time and fade duration
        float alpha = 0.2f - Mathf.Clamp01(currentFadeTime / overlayFadeTime);

        // Update the alpha value of the color
        Color overlayColor = staminaOverlay.color;
        overlayColor.a = alpha;
        staminaOverlay.color = overlayColor;

        // Check if the fade is complete
        if (currentFadeTime >= overlayFadeTime)
        {
            overlayIsFading = false;
            staminaOverlay.gameObject.SetActive(false);
        }
    }
    
    
    

   
}
