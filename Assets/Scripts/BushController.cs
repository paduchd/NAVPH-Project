using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushController : MonoBehaviour
{
    public GameObject eagle;
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            eagle.GetComponent<EagleController>().HidePlayer();
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            eagle.GetComponent<EagleController>().ShowPlayer();
        }
    }
}
