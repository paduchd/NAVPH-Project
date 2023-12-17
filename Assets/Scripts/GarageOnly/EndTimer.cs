using UnityEngine;

public class EndTimer : MonoBehaviour
{
    private bool playerDetected;
    
    // Ends the garage timer when player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerDetected)
        {
            PlayerEventManager.TriggerOnPlayerTimerEscape();
            playerDetected = true;
        }
    }
}
