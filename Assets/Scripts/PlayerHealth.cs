using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health parameters")]
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField] private float healTime = 5.0f; //seconds it take to heal 1 hearth
    private float  timeSinceLastHeal = 0.0f;
    private bool playerIsDead;
    
    [Header("Damage parameters")]
    [SerializeField] private float knockbackForce = 8f;
    private Rigidbody playerRigitbody;
    
    
    [Header("Death parameters")]
    [SerializeField] private float slowMotionTimeScale = 0.1f;
    [SerializeField] private MovementAnimations movementAnimator;
    private float StartFixedDeltaTime;
    private float StartTimeScale;

    
    private void Start()
    {
        playerRigitbody = GetComponent<Rigidbody>();
        StartFixedDeltaTime = Time.fixedDeltaTime;
        StartTimeScale = Time.timeScale;
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
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            movementAnimator.AnimateDeath();
            StartCoroutine(PlayerDeath());
        }
        
        PlayerEventManager.TriggerOnDamaged();
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

    IEnumerator PlayerDeath()
    {
        PlayerEventManager.TriggerOnDeath();
        
        //slow time
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = StartFixedDeltaTime * slowMotionTimeScale;
        
        yield return new WaitForSeconds(0.2f);
        
        //time to normal
        Time.timeScale = StartTimeScale;
        Time.fixedDeltaTime = StartFixedDeltaTime;
        SceneManager.LoadScene("GameOver");
    }
    
}
