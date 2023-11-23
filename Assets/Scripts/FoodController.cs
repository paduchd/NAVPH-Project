using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] private float staminaReplishAmount = 10f;
    [SerializeField] private PlayerStamina playerStamina;
    private bool eaten;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && !eaten)
        {
            Debug.Log("Food");
            playerStamina.IncreaseMax(staminaReplishAmount);
            eaten = true;
            Destroy(gameObject);
        }
    }

}
