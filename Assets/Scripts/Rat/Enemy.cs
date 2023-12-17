using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

// Base script for rat and outsckirts rat
// Handles rats health, death, attacks, getting stunned and damage taking 
public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float deathCooldown = 10f;
    
    private Animator animator;
    private AttackDetection attackDetection;
    
    public bool inAttackCooldown;
    public bool inAttackAnimation;
    public bool isStunned;
    
    private string ATTACK = "Attack";
    private string DIE = "Die";
    
    public static event Action OnRatAttack;
    public static void TriggerOnRatAttack() => OnRatAttack?.Invoke();
    public static event Action OnRatDeath;
    public static void TriggerOnRatDeath() => OnRatDeath?.Invoke();
    public static event Action OnRatHit;
    public static void TriggerOnRatHit() => OnRatHit?.Invoke();
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        attackDetection = GetComponentInChildren<AttackDetection>();
    }
    
    // Damage taking when player attacks and handles death
    public void TakeDamage(int incomingDamage,Transform playerTransform)
    {
        OnRatHit?.Invoke();
        if (health > 0)
        {
            health -= incomingDamage;

            if (health <= 0)
            {
                health = 0;
                StartCoroutine(Death());
            }
        }
    }
    
    //When rat dead triggers dead animation and deletes its self after 10s 
    IEnumerator Death()
    {
        OnRatDeath?.Invoke();
        animator.SetTrigger(DIE);
        yield return new WaitForSeconds(deathCooldown);
        Destroy(this.GameObject());
    }
    
    public void Update()
    {
        //Attack conditions
        if (!isStunned && attackDetection.playerDetected && !inAttackCooldown && health > 0)
        {
            StartCoroutine(Attack());
        }
    }
    
    public void GetStun(float duration)
    {
        StartCoroutine(Stun(duration));
    }
    
    // Handles stun cooldown
    IEnumerator Stun(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);

        isStunned = false;
    }
    
    // Rats attack
    IEnumerator Attack()
    {
        //triggers animation
        inAttackCooldown = true;
        inAttackAnimation = true;
        animator.SetTrigger(ATTACK);
        
        float attackAnimationLength = GetAnimationClipLength("Attack");
        //After 0.4s rat has front legs in the position of inpact
        float timeOfImpact = 0.4f;
        
        //Player can run away in the beginning of attack animation and dont get hit
        yield return new WaitForSeconds(timeOfImpact);
        OnRatAttack?.Invoke();
        if(attackDetection.playerDetected)
            attackDetection.enemyPlayerHealth.TakeDamage(damage, transform);
        
        yield return new WaitForSeconds(attackAnimationLength - timeOfImpact);
        inAttackAnimation = false;
        
        //Start cooldown
        yield return new WaitForSeconds(attackCooldown - attackAnimationLength);
        inAttackCooldown = false;
    }
    
    float GetAnimationClipLength(string clipName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f; 
    }
}