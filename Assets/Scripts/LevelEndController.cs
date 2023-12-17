using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    private bool isLoading = false;
    
    // Trigger scene switching event when colliding with level end object
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("GarageEnd") && !isLoading)
        {
            isLoading = true;
            SceneSwitchEventManager.TriggerSewersSwitch();
        }

        if (col.gameObject.CompareTag("SewersEnd") && !isLoading)
        {
            isLoading = true;
            SceneSwitchEventManager.TriggerOutskirtsSwitch();
        }

        if (col.gameObject.CompareTag("OutskirtsEnd") && !isLoading)
        {
            Debug.Log("Finished outskirts");
            isLoading = true;
            SceneSwitchEventManager.TriggerJunkyardSwitch();
        }
    }
}
