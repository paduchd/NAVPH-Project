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

    [SerializeField] private Collider attackCollider;
    

    private bool isAttacking;
    private float currentAttackCooldown;
    private PlayerHealth enemyPlayerHealth;

    public static event Action OnRatAttack;
    public static event Action OnRatHit;
    public static event Action OnRatDeath;

    private void Start()
    {
        currentAttackCooldown = attackCooldown;
    }

    public void TakeDamage(int incomingDamage)
    {
        isAttacking = true;
        health -= incomingDamage;

        OnRatHit?.Invoke();

        if (health <= 0)
        {
            OnRatDeath?.Invoke();
            Destroy(this.GameObject());
        }
    }

    private bool PlayerInAttackCollider()
    {
        if (enemyPlayerHealth == null)
            return false;

        // this is needed because the collider is on the children gameobject, not the one enemyplayerhealth script is attached to
        return attackCollider.bounds.Intersects(enemyPlayerHealth.GameObject().GetComponentInChildren<Collider>().bounds);
    }
    
    public void Update()
    {
        if (isAttacking && currentAttackCooldown <= 0 && PlayerInAttackCollider())
        {
            OnRatAttack?.Invoke();
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
            isAttacking = true;
        }
        
    }
}