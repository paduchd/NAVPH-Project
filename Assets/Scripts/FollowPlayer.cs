using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform player;
    // Start is called before the first frame update

    private bool isChasing = false;

    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float followDistance;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            float distance = Vector3.Distance(enemy.nextPosition, player.position);
            //Debug.Log(distance);
            if(distance > 1.2 && distance <= followDistance)
            {
                enemy.isStopped = false;
                enemy.acceleration = acceleration;
                enemy.SetDestination(player.position);
                

                //Debug.Log("Chasing");
            }
            else if (distance <= 1.25)
            {
                //Debug.Log("Close fight");
                enemy.velocity = Vector3.zero;
                enemy.acceleration = 0;
                enemy.isStopped = true;
                
                Vector3 lookAtPosition = new Vector3(player.position.x, enemy.transform.position.y, player.position.z);
                enemy.transform.LookAt(lookAtPosition);
            }
            else
            {
                //Debug.Log("Far away");
                enemy.isStopped = true;
            }
        }
    }

    void OnTriggerEnter(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Player") && !isChasing)
        {
            Debug.Log("Entering");
            isChasing = true;
        }

    }
    
    void OnTriggerExit(Collider Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exiting");
        }

    }

    
}
