using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ValveInteraction : MonoBehaviour
{
    // The water whose y-coordinate will be elevated after the interaction
    [SerializeField] private GameObject water;
    
    // Flags related to the interaction
    private bool isInInteractionRange;
    private bool activated;
    private bool isDisabled = true;
    
    // Valve rotation attributes and flags
    [SerializeField] private float rotationSpeed = 0.5f; // Speed of rotation
    private bool isRotating;
    private float totalRotation;
    
    // Event created by the interaction
    public static event Action OnValveInteraction;
    private static void TriggerOnValveInteraction() => OnValveInteraction?.Invoke();

    // Called once each frame
    // OnTriggerStay can be used as well, however it would require collider size and shape change
    // and I find the update more reliable
    void Update()
    {
        // Check whether player is in interaction range of not activated, not disabled valve and pressed E to interact
        if(isInInteractionRange && !activated && !isDisabled && Input.GetKeyDown(KeyCode.E)){
            activated = true;
            isRotating = true;
            
            // Elevation of water level
            Vector3 waterPosition = water.gameObject.transform.position;
            waterPosition.y += 0.2f;
            water.gameObject.transform.position = waterPosition;
            
            // Valve interaction updates the task objective
            ObjectiveManager.Instance.UpdateTaskObjectiveProgress();
            
            // Invoked OnValveInteraction event
            TriggerOnValveInteraction();
        }
        
        // Rotation of valve after the interaction
        if (isRotating)
        {
            float rotationAmount = 360 * Time.deltaTime * rotationSpeed; 
            transform.Rotate(0, rotationAmount, 0, Space.Self); 
            totalRotation += rotationAmount; 

            if (totalRotation >= 360)
            {
                // Ensure the valve completes exactly 360 degrees rotation
                float overshoot = totalRotation - 360;
                transform.Rotate(-overshoot, 0, 0, Space.Self); // Correct any overshoot
                isRotating = false;
            }
        }
        
        // Enables the interaction only when the associated objective is active
        if (isDisabled && ObjectiveManager.Instance.objectives.Find(objective => !objective.completed).description == "Loosen the valves")
        {
            isDisabled = false;
        }
        
    }
    
    // On entering the trigger which represents interaction range of the valve
    private void OnTriggerEnter(Collider collisionObject){
        
        // Check if its the player who entered the trigger
        if (collisionObject.gameObject.CompareTag("Player") && !activated && !isDisabled){
            isInInteractionRange = true;
            
            // Triggers OnInteractionEnter event, which displays possible interaction in the UI
            PlayerEventManager.TriggerOnInteractionEnter();
        }
    }

    // On exiting the trigger which represents interaction range of the valve
    private void OnTriggerExit(Collider collisionObject){
        
        // Check if its the player who exited the trigger
        if (collisionObject.gameObject.CompareTag("Player")){
            isInInteractionRange = false;
            
            // Triggers OnInteractionExit event, which hides possible interaction in the UI
            PlayerEventManager.TriggerOnInteractionExit();
        }
    }
}
