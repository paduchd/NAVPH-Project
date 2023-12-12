using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BushController : MonoBehaviour
{
    public GameObject eagle;

    public static event Action OnBushEnter;
    public static void TriggerOnBushEnter() => OnBushEnter?.Invoke();
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            TriggerOnBushEnter();
            eagle.GetComponent<Eagle>().HidePlayer();
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            TriggerOnBushEnter();
            eagle.GetComponent<Eagle>().ShowPlayer();
        }
    }
}
