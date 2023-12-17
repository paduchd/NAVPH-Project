using UnityEngine;
using System;

public class BushController : MonoBehaviour
{
    public GameObject eagle;

    public static event Action OnBushEnter;
    public static void TriggerOnBushEnter() => OnBushEnter?.Invoke();

    // Ignores collisions with player's attack triggers
    private void Start()
    {
        Physics.IgnoreLayerCollision(9, 10);
    }

    // When player enters a bush it hides the player from the eagle
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            TriggerOnBushEnter();
            eagle.GetComponent<Eagle>().HidePlayer();
        }
    }
    
    // Shows the player again when he leaves the bush
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            TriggerOnBushEnter();
            eagle.GetComponent<Eagle>().ShowPlayer();
        }
    }
}
