using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("GarageEnd"))
        {
            Debug.Log("Player escaped the garage");
            SceneSwitchEventManager.TriggerSewersSwitch();
        }

        if (col.gameObject.CompareTag("SewersEnd"))
        {
            Debug.Log("Player escaped the sewers");
            SceneSwitchEventManager.TriggerOutskirtsSwitch();
        }

        if (col.gameObject.CompareTag("OutskirtsEnd"))
        {
            Debug.Log("Player escaped the outskirts");
            SceneSwitchEventManager.TriggerJunkyardSwitch();
        }
    }
}
