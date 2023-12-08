using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    [SerializeField] private float damageFadeTime = 3.0f;
    private Image imageOverlay;
    private float currentFadeTime;

    private void OnEnable()
    {
        PlayerEventManager.OnDamaged += StartFading;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnDamaged -= StartFading;
    }
    
    void Start()
    {
        imageOverlay = GetComponent<Image>();
    }
    
    void Update()
    {
        if (imageOverlay.enabled)
        {
            DamageOverlayFading();
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
        float alpha = 0.3f - Mathf.Clamp01(currentFadeTime / damageFadeTime);

        // Update the alpha value of the color
        Color overlayColor = imageOverlay.color;
        overlayColor.a = alpha;
        imageOverlay.color = overlayColor;

        // Check if the fade is complete
        if (currentFadeTime >= damageFadeTime)
        {
            imageOverlay.enabled = false;
        }
    }
}
