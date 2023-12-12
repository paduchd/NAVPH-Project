using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public ObjectiveManager.ObjectiveType type;
    public string description;
    public bool completed;

    // Additional properties for task objectives
    public int targetCount;
    public int currentCount;

    public Objective(ObjectiveManager.ObjectiveType type, string description, int targetCount = 0)
    {
        this.type = type;
        this.description = description;
        this.completed = false;

        // Initialize task objective properties
        if (type == ObjectiveManager.ObjectiveType.Task)
        {
            this.targetCount = targetCount;
            this.currentCount = 0;
        }
    }
}
