using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

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

    public bool isStunned = false;
    
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

    IEnumerator Stun(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);

        isStunned = false;
    }
    
    IEnumerator Attack()
    {
        inAttackCooldown = true;
        inAttackAnimation = true;
        animator.SetTrigger(ATTACK);
        
        float attackAnimationLength = GetAnimationClipLength("Attack");
        float timeOfImpact = 0.4f;
        
        //Player can run away in the beginning of attack animation and dont get hit
        yield return new WaitForSeconds(timeOfImpact);
        OnRatAttack?.Invoke();
        if(attackDetection.playerDetected)
            attackDetection.enemyPlayerHealth.TakeDamage(damage, transform);
        
        yield return new WaitForSeconds(attackAnimationLength - timeOfImpact);
        inAttackAnimation = false;
        
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