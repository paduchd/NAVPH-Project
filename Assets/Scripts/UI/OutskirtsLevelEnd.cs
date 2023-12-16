using System;
using UnityEngine;
using TMPro;

public class OutskirtsLevelEnd : MonoBehaviour
{
    [SerializeField] private GameObject endWall;
    [SerializeField] private TextMeshProUGUI urgentMessage;
    
    private int foodEaten = 0;
    private bool canBeFinished = false;

    private void OnEnable()
    {
        PlayerEventManager.OnFoodEaten += FoodCollected;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            urgentMessage.text = "Collect all food before escaping";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            urgentMessage.text = "";
        }
    }

    private void FoodCollected(bool b)
    {
        foodEaten += 1;
    }

    private void Update()
    {
        if (foodEaten == 5 && !canBeFinished)
        {
            endWall.SetActive(false);
            canBeFinished = true;
        }
    }
}
