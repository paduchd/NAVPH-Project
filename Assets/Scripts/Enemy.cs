using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] private int health;

    [SerializeField] private int damage;

    [SerializeField] private float attackCooldown;

    private bool isAttacking;
    private float currentAttackCooldown;
    private PlayerHealth enemyPlayerHealth;

    private void Start()
    {
        currentAttackCooldown = attackCooldown;
    }

    public void TakeDamage(int incomingDamage)
    {
        isAttacking = true;
        health -= incomingDamage;
        
        if (health <= 0)
            Destroy(this.GameObject());
    }

    public void Update()
    {
        if (isAttacking && currentAttackCooldown <= 0)
        {
            enemyPlayerHealth.TakeDamage(damage, transform);
            currentAttackCooldown = attackCooldown;
        }
        else
        {
            currentAttackCooldown -= Time.deltaTime;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            enemyPlayerHealth = other.transform.parent.GetComponent<PlayerHealth>();
            Debug.Log(enemyPlayerHealth);
        }
        
    }
}