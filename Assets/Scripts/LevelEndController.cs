using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("GarageEnd"))
        {
            Debug.Log("Player escaped the garage");
            PlayerEventManager.TriggerOnGarageEscape();
        }

        if (col.gameObject.CompareTag("SewersEnd"))
        {
            Debug.Log("Player escaped the sewers");
            PlayerEventManager.TriggerOnSewersEscape();
        }

        if (col.gameObject.CompareTag("OutskirtsEnd"))
        {
            Debug.Log("Player escaped the outskirts");
            PlayerEventManager.TriggerOnOutskirtsEscape();
        }
    }
}
