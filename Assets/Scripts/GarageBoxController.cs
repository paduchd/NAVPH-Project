using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageBoxController : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("GarageFloor"))
        {
            Debug.Log("Box collided with the ground");
            PlayerEventManager.TriggerOnBoxFall();
        }
    }
}
