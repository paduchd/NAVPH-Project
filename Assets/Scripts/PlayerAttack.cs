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

    private Enemy[] currentAOETargets;
    private Enemy currentSingleAttackTarget;

    [SerializeField] private float singleAttackCooldown;
    private float currentSingleAttackCooldown;

    private void Start()
    {
        currentSingleAttackCooldown = singleAttackCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && SingleAttackCollider.bounds.Intersects(other.bounds))
        {
            Debug.Log("Enemy in single attack!");
            currentSingleAttackTarget = other.GetComponent<Enemy>();
        }
        // if(other.CompareTag("Enemy") && AOEAttackCollider.bounds.Intersects(other.bounds))
        //     Debug.Log("Enemy in AOE");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack())
        { 
            currentSingleAttackTarget?.TakeDamage(5);
            currentSingleAttackCooldown -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0) && !CanAttack())
        {
            Debug.Log("On cooldown!");
        }

        if (!CanAttack() && currentSingleAttackCooldown > 0)
        {
            currentSingleAttackCooldown -= Time.deltaTime;
        }
        else if (!CanAttack() && currentSingleAttackCooldown <= 0)
        {
            currentSingleAttackCooldown = singleAttackCooldown;
        }
        
    }

    private bool CanAttack()
    {
        return currentSingleAttackCooldown == singleAttackCooldown;
    }
}
