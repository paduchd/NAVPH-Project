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
    
    private string ATTACK = "Attack";
    private string DIE = "Die";
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        attackDetection = GetComponentInChildren<AttackDetection>();
    }

    public void TakeDamage(int incomingDamage,Transform playerTransform)
    {
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
        animator.SetTrigger(DIE);
        yield return new WaitForSeconds(deathCooldown);
        Destroy(this.GameObject());
    }
    
    public void Update()
    {
        //Attack conditions
        if (attackDetection.playerDetected && !inAttackCooldown && health > 0)
        {
            StartCoroutine(Attack());
        }
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