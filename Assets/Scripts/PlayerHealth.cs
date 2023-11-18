using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health parameters")]
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField] private HealthBarController healthBarController;
    [SerializeField] private float healTime = 5.0f; //seconds it take to heal 1 hearth
    private float timeSinceLastHeal = 0.0f;
    
    
    [Header("Damage parameters")]
    [SerializeField] private Image damageOverlay;
    [SerializeField] private float damageFadeTime = 3.0f; // Time it takes for the overlay to fade out
    private float currentFadeTime = 0.0f;
    private bool isTakingDamage = false;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2);
        }
        
        if (isTakingDamage)
        {
            DamageOverlayFading();
        }
        
        HealOverTime();
    }
    
    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        timeSinceLastHeal = 0; //reset healing
        ShowDamageOverlay();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //death logic
        }
        healthBarController.UpdateHeartStates();
        
    }
    
    private void ShowDamageOverlay()
    {
        isTakingDamage = true;
        currentFadeTime = 0.0f;
        damageOverlay.gameObject.SetActive(true);
    }
    
    private void DamageOverlayFading()
    {
        currentFadeTime += Time.deltaTime;

        // Calculate the alpha value based on time and fade duration
        float alpha = 0.3f - Mathf.Clamp01(currentFadeTime / damageFadeTime);

        // Update the alpha value of the color
        Color overlayColor = damageOverlay.color;
        overlayColor.a = alpha;
        damageOverlay.color = overlayColor;

        // Check if the fade is complete
        if (currentFadeTime >= damageFadeTime)
        {
            isTakingDamage = false;
            damageOverlay.gameObject.SetActive(false);
        }
    }

    private void HealOverTime()
    {
        // Only heal over time if the player is alive and dont have full health
        if (currentHealth > 0 && currentHealth < maxHealth)
        {
            timeSinceLastHeal += Time.deltaTime;
            
            if (timeSinceLastHeal >= healTime)
            {
                timeSinceLastHeal = 0.0f;
                currentHealth += 1; 
                healthBarController.UpdateHeartStates();
            }
        }
        
    }
    
}
