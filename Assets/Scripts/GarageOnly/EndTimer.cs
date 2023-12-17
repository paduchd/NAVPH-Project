using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndTimer : MonoBehaviour
{
    private bool playerDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerDetected)
        {
            PlayerEventManager.TriggerOnPlayerTimerEscape();
            playerDetected = true;
        }
    }
}
