using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageBoxController : MonoBehaviour
{
    private bool floorHit;
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("GarageFloor") && !floorHit)
        {
            PlayerEventManager.TriggerOnBoxFall();
            floorHit = true;
        }
    }
}
