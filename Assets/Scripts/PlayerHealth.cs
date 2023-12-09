using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health parameters")]
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField] private float healTime = 5.0f; //seconds it take to heal 1 hearth
    private float  timeSinceLastHeal = 0.0f;
    
    [Header("Damage parameters")]
    [SerializeField] private float knockbackForce = 8f;
    private Rigidbody playerRigitbody;


    private void Start()
    {
        playerRigitbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TakeDamage(1);
        }
        
        HealOverTime();
    }
    
    
    public void TakeDamage(int amount,Transform attackerTransform = null)
    {
        currentHealth -= amount;
        timeSinceLastHeal = 0; //reset healing
        
        //knockback player
        if (attackerTransform != null)
        {
            Vector3 knockbackDirection = transform.position - attackerTransform.position;
            knockbackDirection.Normalize();
            playerRigitbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
        
        PlayerEventManager.TriggerOnDamaged();
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //death logic
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
                PlayerEventManager.TriggerOnHealthIncrease();
            }
        }
        
    }
    
}
