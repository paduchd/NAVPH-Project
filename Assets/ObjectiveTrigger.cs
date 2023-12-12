using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (or any other relevant object)
        if (other.CompareTag("Player") && !triggered)
        {
            // Call a method in the ObjectiveManager to update the objective
            ObjectiveManager.Instance.UpdateTaskObjectiveProgress();
            triggered = true;
        }
    }
}
