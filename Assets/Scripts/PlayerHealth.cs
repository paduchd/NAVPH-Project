using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //logic on damage taken or event can be emited
        //update Health UI

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //death logic
        }
    }

    private void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        //update Health UI
    }
    
}
