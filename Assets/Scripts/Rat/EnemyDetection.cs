using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For detecting player if he is in range to follow him
// Sits on rats child object EnemyDetection with collider for that
// by this attack detection and enemy detection dont trigger for player at the same time
public class EnemyDetection : MonoBehaviour
{
    //Enemy script can have access to detectedPlayer object
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
