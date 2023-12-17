using UnityEngine;
// For detecting player if he is in range to attack him
// Sits on rats child object AttackDetection with collider for that
// by this attack detection and enemy detection dont trigger for player at the same time

public class AttackDetection : MonoBehaviour
{
    // Enemy script have access to player health to take damage
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
