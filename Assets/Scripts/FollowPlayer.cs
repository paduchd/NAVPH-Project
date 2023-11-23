using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform player;
    // Start is called before the first frame update

    private bool IsChasing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsChasing)
        {
            enemy.SetDestination(player.position);
        }
        
    }

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entering");
            IsChasing = true;
        }

    }
    void OnTriggerExit(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exiting");
            IsChasing = false;
        }

    }

    
}
