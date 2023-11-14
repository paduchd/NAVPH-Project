using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
	public GameObject box;

	private void OnTriggerEnter(Collider other)
    {
        box.GetComponent<BoxTrapController>().DisableTimer();
    }
}
