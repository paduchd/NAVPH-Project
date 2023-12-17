using System;
using UnityEngine;
using TMPro;

// Manages the end part of the outskirts level, shows text when player is in proximity of end
public class OutskirtsLevelEnd : MonoBehaviour
{
    [SerializeField] private GameObject endWall;
    [SerializeField] private TextMeshProUGUI urgentMessage;
    
    private int foodEaten = 0;
    private bool canBeFinished = false;

    // Subscribing to events
    private void OnEnable()
    {
        PlayerEventManager.OnFoodEaten += FoodCollected;
    }

    // Unsubscribing to events
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && !canBeFinished)
        {
            urgentMessage.text = "Collect all food before escaping";
        }
    }

    // Clears text after the player is out of the ending range
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            urgentMessage.text = "";
        }
    }

    // Updates the current status of the food
    private void FoodCollected(bool b)
    {
        foodEaten += 1;
    }
    
    // after the scene is finished, the ending can be approached

    private void Update()
    {
        if (foodEaten == 5 && !canBeFinished)
        {
            endWall.SetActive(false);
            canBeFinished = true;
        }
    }
}
