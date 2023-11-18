using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private PlayerHealth playerHealth;

    private List<HeartStateController> hearts = new List<HeartStateController>();

    private void CreateHearts()
    {
        ClearBar();
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform, true);
            HeartStateController heartState = newHeart.GetComponent<HeartStateController>();
            hearts.Add(heartState);
        }
        
    }
    
    private void ClearBar()
    {
        hearts = new List<HeartStateController>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

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

    private void Start()
    {
        CreateHearts();
        UpdateHeartStates();
    }

    private void Update()
    {
        
    }
}
