using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Manages the part of UI which showcases the possible interaction
public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    
    // Subscribing to events
    private void OnEnable()
    {
        PlayerEventManager.OnInteractionEnter += DisplayInteraction;
        PlayerEventManager.OnInteractionExit += HideInteraction;
    }
    
    // Unsubscribing to events
    private void OnDisable()
    {
        PlayerEventManager.OnInteractionEnter -= DisplayInteraction;
        PlayerEventManager.OnInteractionExit -= HideInteraction;
    }

    // Showcases the text, when the interaction is possible (player in range)
    private void DisplayInteraction()
    {
        interactionText.text = "Press [E] to interact.";
        interactionText.enabled = true;
    }

    // Disables the text, when the interaction is not possible (player out of range)
    private void HideInteraction()
    {
        interactionText.enabled = false;
    }
    
}
