using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndTimer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerEventManager.TriggerOnPlayerTimerEscape();
    }
}
