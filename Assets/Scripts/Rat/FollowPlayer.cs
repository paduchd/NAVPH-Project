using UnityEngine;
using UnityEngine.AI;

// Rat logic for following player and its animations with navmesh agent
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float followDistance;
    [SerializeField] private float stopDistance = 0.3f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private NavMeshAgent enemyAgent;
    private Animator animator;
    private Enemy enemy;
    private EnemyDetection detection;
    
    private bool isChasing;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyAgent = GetComponent<NavMeshAgent>();
        detection = GetComponentInChildren<EnemyDetection>();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = detection.detectedPlayer.transform.position - enemyAgent.transform.position;
        directionToPlayer.y = 0; // Ensure the enemy only rotates around the y-axis

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        enemyAgent.transform.rotation = Quaternion.Slerp(enemyAgent.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    private void Update()
    {
        // If rat not dead and detects player in range to follow
        if (detection.isDetected && enemy.health > 0)
        {
            float distance = Vector3.Distance(enemyAgent.nextPosition, detection.detectedPlayer.transform.position);
            
            // If rat is stunned the he stops
            if (enemy.isStunned)
            {
                enemyAgent.isStopped = true;
                enemyAgent.velocity = Vector3.zero;
                enemyAgent.acceleration = 0;
            }
            
            //If rat is not too close for attacking and not too far to stop follow him and is not attacking the player
            else if(distance > stopDistance && distance <= followDistance && !enemy.inAttackAnimation)
            {
                //Follow player
                enemyAgent.isStopped = false;
                enemyAgent.acceleration = acceleration;
                enemyAgent.SetDestination(detection.detectedPlayer.transform.position);
                
                RotateTowardsPlayer();
                
            }
            
            //If rat too far to keep following the player than he stops
            else if(distance > followDistance)
            {
                enemyAgent.isStopped = true;
                
                // When player far await enemy still can have remaining velocity
                if (enemyAgent.velocity.magnitude == 0)
                {
                    detection.isDetected = false;
                }
            }
            else 
            {
                if(!enemy.inAttackAnimation)
                    RotateTowardsPlayer();
                enemyAgent.velocity = Vector3.zero;
                enemyAgent.acceleration = 0;
                enemyAgent.isStopped = true;
            }
            
            AnimateSpeed();
        }
        
    }
    
    //How rat animation looks can be set with this faction based on nav mash agent velocity
    private void AnimateSpeed()
    {
        // Calculate the normalized speed (assuming maximum speed is the agent's speed when fully accelerated)
        float normalizedSpeed = enemyAgent.velocity.magnitude / enemyAgent.speed; // enemy.speed is the maximum speed
        normalizedSpeed = Mathf.Clamp(normalizedSpeed * 10, 0, 10); // Scale and clamp to fit between 0 and 10 because of animation
                
        animator.SetFloat("Speed", normalizedSpeed);
    }
    
}
