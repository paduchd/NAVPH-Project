using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ValveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private TextMeshProUGUI UIInteractionText;
    private bool isInInteractionRange;
    private bool activated;
    private bool isDisabled = true;
    
    [SerializeField] private float rotationSpeed = 0.5f; // Speed of rotation
    private bool isRotating;
    private float totalRotation;
    
    public static event Action OnValveInteraction;
    private static void TriggerOnValveInteraction() => OnValveInteraction?.Invoke();

    
    // OnTriggerStay can be used as well, however it would require collider size and shape change
    void Update()
    {
        // Interaction check
        if(isInInteractionRange && !activated && !isDisabled &&Input.GetKeyDown(KeyCode.E)){
            activated = true;
            isRotating = true;
            
            Vector3 waterPosition = water.gameObject.transform.position;
            waterPosition.y += 0.2f;
            water.gameObject.transform.position = waterPosition;
            
            ObjectiveManager.Instance.UpdateTaskObjectiveProgress();
            TriggerOnValveInteraction();
        }
        
        
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
        
        // Objective check
        if (isDisabled && ObjectiveManager.Instance.objectives.Find(objective => !objective.completed).description ==
            "Loosen the valves")
        {
            isDisabled = false;
        }
        
    }
    
    private void OnTriggerEnter(Collider collisionObject){
        if (collisionObject.gameObject.CompareTag("Player")){
            isInInteractionRange = true;
            UIInteractionText.enabled = true;
            UIInteractionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collisionObject){
        if (collisionObject.gameObject.CompareTag("Player")){
            isInInteractionRange = false;
            UIInteractionText.gameObject.SetActive(false);
        }
    }
}
