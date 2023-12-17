using System.Collections;
using UnityEngine;

// Outskirt big rat script as extension of enemy
// Handles spawning food on death, performing idle reaction when 
public class OutskirtsRat : MonoBehaviour, IDamageable
{
    public Transform player;
    public GameObject rewardFood;
    private AttackDetection attackDetection;
    private Animator animator;
    private bool isAlive = true;
    private bool idleCooldown;

    private void Start()
    {
        attackDetection = GetComponentInChildren<AttackDetection>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Enemy.OnRatDeath += SpawnReward;
    }
    
    private void OnDisable()
    {
        Enemy.OnRatDeath -= SpawnReward;
    }
    
    // Loooking at player
    private void Update()
    {
        if (isAlive)
        {
            transform.LookAt(player);
            if (!attackDetection.playerDetected && !idleCooldown)
            {
                StartCoroutine(Idle());
            }
        }
    }
    
    // Coroutine for idle animation every 10s when player is not detected in attack range
    IEnumerator Idle()
    {
        idleCooldown = true;
        if (!attackDetection.playerDetected)
        {
            animator.SetTrigger("Idle Reaction");
        }

        yield return new WaitForSeconds(10);
        idleCooldown = false;
    }
    
    // Spawns food when rat dead
    private void SpawnReward()
    {
        isAlive = false;
        rewardFood.SetActive(true);
    }
}
