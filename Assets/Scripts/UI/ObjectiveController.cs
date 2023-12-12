using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveBar;
    // Start is called before the first frame update
    
    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveUpdate += UpdateObjective;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveUpdate -= UpdateObjective;
    }

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
