using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void SpawnReward()
    {
        isAlive = false;
        rewardFood.SetActive(true);
    }
}
