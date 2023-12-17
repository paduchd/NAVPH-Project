using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller for food in outskirts which replenishes player stamina
public class FoodController : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private float staminaReplishAmount = 10f;
    [SerializeField] private PlayerStamina playerStamina;
    private bool eaten;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !eaten)
        {
            ObjectiveManager.Instance.UpdateTaskObjectiveProgress();
            playerStamina.IncreaseMax(staminaReplishAmount);
            
            eaten = true;
            Destroy(gameObject);
        }
    }
}
