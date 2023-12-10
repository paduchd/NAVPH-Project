using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttack : MonoBehaviour
{
    
    public Collider SingleAttackCollider;
    public Collider AOEAttackCollider;

    private List<Enemy> currentAOETargets;
    private Enemy currentSingleAttackTarget;

    [SerializeField] private float singleAttackCooldown;
    [SerializeField] private float aoeAttackCooldown;
    private float currentSingleAttackCooldown;
    private float currentAOEAttackCooldown;

    private void Start()
    {
        currentSingleAttackCooldown = singleAttackCooldown;
        currentAOEAttackCooldown = aoeAttackCooldown;
        currentAOETargets = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && SingleAttackCollider.bounds.Intersects(other.bounds))
        {
            Debug.Log("Enemy in single attack!");
            currentSingleAttackTarget = other.GetComponent<Enemy>();
        }

        if (other.CompareTag("Enemy") && AOEAttackCollider.bounds.Intersects(other.bounds))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            Debug.Log(currentAOETargets.Count);
            if (!currentAOETargets.Contains(enemy))
            {
                currentAOETargets.Add(other.GetComponent<Enemy>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanSingleAttack())
        {
            if (currentSingleAttackTarget != null)
            {
                currentSingleAttackTarget?.TakeDamage(5);
                currentSingleAttackCooldown -= Time.deltaTime;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !CanSingleAttack())
        {
            Debug.Log("ST cooldown!");
        }

        if (!CanSingleAttack() && currentSingleAttackCooldown > 0)
        {
            currentSingleAttackCooldown -= Time.deltaTime;
        }
        else if (!CanSingleAttack() && currentSingleAttackCooldown <= 0)
        {
            currentSingleAttackCooldown = singleAttackCooldown;
        }
        
        if (Input.GetMouseButtonDown(1) && CanAOEAttack())
        {
            foreach (var target in currentAOETargets)
            {
                Debug.Log("Giving damage");
                if (target != null)
                {
                    target.TakeDamage(5);
                    currentAOEAttackCooldown -= Time.deltaTime;   
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && !CanAOEAttack())
        {
            Debug.Log("AOE Cooldown!");
        }
        
        if (!CanAOEAttack() && currentAOEAttackCooldown > 0)
        {
            currentAOEAttackCooldown -= Time.deltaTime;
        }
        else if (!CanAOEAttack() && currentAOEAttackCooldown <= 0)
        {
            currentAOEAttackCooldown = aoeAttackCooldown;
        }
        
    }

    private bool CanSingleAttack()
    {
        return currentSingleAttackCooldown == singleAttackCooldown;
    }

    private bool CanAOEAttack()
    {
        return currentAOEAttackCooldown == aoeAttackCooldown;
    }
}
