using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages the stamina bar in UI
public class StaminaBarController : MonoBehaviour
{
    [SerializeField] private Image staminaBarSlider;
    [SerializeField] private Image staminaBarBackground;
    [SerializeField] private PlayerStamina playerStamina;

    // Subscribing to events
    private void OnEnable()
    {
        PlayerEventManager.OnStaminaUpdate += UpdateSliderWidth;
        PlayerEventManager.OnFoodEaten += UpdateBackgroundWidth;
    }
    
    // Unsubscribing to events
    private void OnDisable()
    {
        PlayerEventManager.OnStaminaUpdate -= UpdateSliderWidth;
        PlayerEventManager.OnFoodEaten -= UpdateBackgroundWidth;
    }

    private void Start()
    {
        UpdateBackgroundWidth();
    }
    
    private void UpdateBackgroundWidth(bool maxStaminaIncreased)
    {
        if (maxStaminaIncreased) UpdateBackgroundWidth();
    }
    
    // The stamina bar length is determined by player's max stamina
    private void UpdateBackgroundWidth()
    {
        staminaBarBackground.rectTransform.sizeDelta = new Vector2((458 * playerStamina.maxStamina) / 100 , staminaBarBackground.rectTransform.sizeDelta.y);
    }


    private void UpdateSliderWidth()
    {
        staminaBarSlider.fillAmount = playerStamina.currentStamina / playerStamina.maxStamina;
    }
  
}
