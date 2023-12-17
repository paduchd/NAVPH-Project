using UnityEngine;

public class GarageBoxController : MonoBehaviour
{
    private bool floorHit;
    
    // Sends an event when box in garage falls on the ground
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("GarageFloor") && !floorHit)
        {
            PlayerEventManager.TriggerOnBoxFall();
            floorHit = true;
        }
    }
}
