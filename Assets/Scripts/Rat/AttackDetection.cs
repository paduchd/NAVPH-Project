using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public PlayerHealth enemyPlayerHealth;
    public bool playerDetected;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            enemyPlayerHealth = other.transform.parent.GetComponent<PlayerHealth>();
            playerDetected = true;
        }
        Debug.Log("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
        Debug.Log("Exit");
    }
}
