using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushController : MonoBehaviour
{
    public GameObject eagle;
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            eagle.GetComponent<Eagle>().HidePlayer();
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            eagle.GetComponent<Eagle>().ShowPlayer();
        }
    }
}
