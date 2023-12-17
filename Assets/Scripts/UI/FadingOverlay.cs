using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fading overlays for UI after eating food and getting damage
public class FadingOverlay : MonoBehaviour
{
    [SerializeField] private float fadeTime = 3.0f;
    [Range(0, 1)] [SerializeField] private float startAlphaValue = 0.3f;
    private Image imageOverlay;
    private float currentFadeTime;
    [SerializeField] private OverlayMode overlayMode;
    
    // The overlay is either for stamina or damage
    private enum OverlayMode
    {
        Stamina = 0,
        Damage = 1,
    }
    
    // Subscribing to events
    private void OnEnable()
    {
        if (overlayMode == OverlayMode.Stamina)
        {
            PlayerEventManager.OnFoodEaten += StartFading;
        } 
        else if (overlayMode == OverlayMode.Damage)
        {
            PlayerEventManager.OnDamaged += StartFading;
            
        }
    }
    
    // Unsubscribing to events
    private void OnDisable()
    {
        if (overlayMode == OverlayMode.Stamina)
        {
            PlayerEventManager.OnFoodEaten -= StartFading;
        } 
        else if (overlayMode == OverlayMode.Damage)
        {
            PlayerEventManager.OnDamaged -= StartFading;
        }
    }
    
    void Start()
    {
        imageOverlay = GetComponent<Image>();
        Color overlayColor = imageOverlay.color;
        overlayColor.a = startAlphaValue;
        imageOverlay.color = overlayColor;
    }
    
    void Update()
    {
        if (imageOverlay.enabled)
        {
            DamageOverlayFading();
        }
    }

    private void StartFading(bool staminaIncreased)
    {
        if (staminaIncreased)
        {
            currentFadeTime = 0.0f;
            imageOverlay.enabled = true;
        }
    }

    private void StartFading()
    {
        currentFadeTime = 0.0f;
        imageOverlay.enabled = true;
    }
    
    private void DamageOverlayFading()
    {
        currentFadeTime += Time.deltaTime;

        // Calculate the alpha value based on time and fade duration
        float alpha = startAlphaValue - Mathf.Clamp01(currentFadeTime / fadeTime);

        // Update the alpha value of the color
        Color overlayColor = imageOverlay.color;
        overlayColor.a = alpha;
        imageOverlay.color = overlayColor;

        // Check if the fade is complete
        if (currentFadeTime >= fadeTime)
        {
            imageOverlay.enabled = false;
        }
    }
}
