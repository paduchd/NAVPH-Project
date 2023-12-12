using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    
    public static event Action<Objective> OnObjectiveUpdate;
    public static void TriggerOnObjectiveUpdate(Objective objective) => OnObjectiveUpdate?.Invoke(objective);

    public enum ObjectiveType
    {
        Simple,
        Task
    }
    
    public List<Objective> objectives = new List<Objective>();
    
    void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Initialize objectives for the current scene
        //InitializeObjectives();

        // Update the UI with the first objective
        if (objectives.Count > 0)
        {
            TriggerOnObjectiveUpdate(objectives[0]);
            Debug.Log("First quest on screen");
            Debug.Log(objectives[0].description);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateTaskObjectiveProgress();
        }
    }
    
    public void CompleteObjective(Objective objective)
    {
        // Handle objective completion
        objective.completed = true;

        // Trigger events or actions related to completing the objective
        // ...

        // Check if there are more objectives
        int index = objectives.IndexOf(objective);
        if (index < objectives.Count - 1)
        {
            // Move to the next objective
            Objective nextObjective = objectives[index + 1];

            // Start the next objective
            Debug.Log("SOMTU");
            Debug.Log($"Current Count: {nextObjective.currentCount}, Target Count: {nextObjective.targetCount}");
            TriggerOnObjectiveUpdate(nextObjective);
        }
        else
        {
            // All objectives completed for the current scene
            // Implement logic for scene transition or other actions
            // ...
        }
    }
    
    
    public void UpdateTaskObjectiveProgress()
    {
        // Find the current task objective
        Objective taskObjective = objectives.Find(objective => !objective.completed);

        if (taskObjective != null)
        {
            // Check if the entire task objective is now completed
            if (taskObjective.currentCount + 1 >= taskObjective.targetCount)
            {
                //taskObjective.currentCount = taskObjective.targetCount;  // Ensure currentCount doesn't exceed targetCount
                CompleteObjective(taskObjective);
            }
            else
            {
                // Update the progress
                taskObjective.currentCount++;
                // Update UI with the current progress
                TriggerOnObjectiveUpdate(taskObjective);
                Debug.Log(taskObjective.description + taskObjective.currentCount.ToString());
            }
        }

    }
}
