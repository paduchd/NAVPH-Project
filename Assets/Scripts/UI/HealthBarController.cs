using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the health bar of the player
public class HealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private PlayerHealth playerHealth;
    private List<HeartStateController> hearts;
    private int oldCurrentHealth;

    // Initialization of the health bar
    private void Start()
    {
        CreateHearts();
        UpdateHeartStates();
    }

    // Subscribing to events
    private void OnEnable()
    {
        PlayerEventManager.OnHealthIncrease += UpdateHeartStates;
        PlayerEventManager.OnDamaged += UpdateHeartStates;
    }

    // Unsubscribing to events
    private void OnDisable()
    {
        PlayerEventManager.OnHealthIncrease -= UpdateHeartStates;
        PlayerEventManager.OnDamaged -= UpdateHeartStates;
    }
    
    // Creates health bar based on the player's max health
    private void CreateHearts()
    {
        ClearBar();
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform, true);
            HeartStateController heartState = newHeart.GetComponent<HeartStateController>();
            hearts.Add(heartState);
            Debug.Log("Added");
        }
        
    }
    
    // Deletes the health bar
    private void ClearBar()
    {
        hearts = new List<HeartStateController>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // Keeps the healthbar accurate with player's current health
    public void UpdateHeartStates()
    
    {
        for (int i = 0; i < playerHealth.currentHealth; i++)
        {
            hearts[i].SetHeartState(HeartState.Full);
        }

        for (int i = playerHealth.currentHealth; i < playerHealth.maxHealth; i++)
        {
            hearts[i].SetHeartState(HeartState.Empty);
        }
    }
}
