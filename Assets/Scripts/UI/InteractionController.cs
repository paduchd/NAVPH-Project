using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    private void OnEnable()
    {
        PlayerEventManager.OnInteractionEnter += DisplayInteraction;
        PlayerEventManager.OnInteractionExit += HideInteraction;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnInteractionEnter -= DisplayInteraction;
        PlayerEventManager.OnInteractionExit -= HideInteraction;
    }

    private void DisplayInteraction()
    {
        interactionText.text = "Press [E] to interact.";
        interactionText.enabled = true;
        //interactionText.gameObject.SetActive(true);
    }

    private void HideInteraction()
    {
        interactionText.enabled = false;
        //interactionText.gameObject.SetActive(false);
    }
    
}
