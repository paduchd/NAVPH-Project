using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Responsible for objective display on UI
public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveBar;
    
    // Subscribing to events
    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveUpdate += UpdateObjective;
    }

    // Unsubscribing to events
    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveUpdate -= UpdateObjective;
    }

    // Updates the objectives text with current status
    private void UpdateObjective(Objective objective)
    {
        if (objective.type == ObjectiveManager.ObjectiveType.Simple)
        {
            objectiveBar.text = objective.description;
        }
        else if (objective.type == ObjectiveManager.ObjectiveType.Task)
        {
            objectiveBar.text = objective.description + " " + objective.currentCount + "/" + objective.targetCount;

        }
    }
    
}
