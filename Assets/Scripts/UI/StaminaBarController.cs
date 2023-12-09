using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{
    [SerializeField] private Image staminaBarSlider;
    [SerializeField] private Image staminaBarBackground;
    [SerializeField] private PlayerStamina playerStamina;

    private void OnEnable()
    {
        PlayerEventManager.OnStaminaUpdate += UpdateSliderWidth;
        PlayerEventManager.OnFoodEaten += UpdateBackgroundWidth;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnStaminaUpdate -= UpdateSliderWidth;
    }

    private void Start()
    {
        UpdateBackgroundWidth();
    }

    private void UpdateBackgroundWidth(bool maxStaminaIncreased)
    {
        if (maxStaminaIncreased) UpdateBackgroundWidth();
    }
    
    private void UpdateBackgroundWidth()
    {
        staminaBarBackground.rectTransform.sizeDelta = new Vector2((458 * playerStamina.maxStamina) / 100 , staminaBarBackground.rectTransform.sizeDelta.y);
    }


    private void UpdateSliderWidth()
    {
        staminaBarSlider.fillAmount = playerStamina.currentStamina / playerStamina.maxStamina;
    }
  
}
