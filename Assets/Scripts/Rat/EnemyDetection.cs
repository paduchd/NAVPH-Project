using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameObject detectedPlayer;
    public bool isDetected;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            detectedPlayer = collision.gameObject;

            isDetected = true;
        }

    }
}
