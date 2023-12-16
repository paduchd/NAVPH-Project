using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttack : MonoBehaviour
{
    [Header("Single Attack")]
    public Collider singleAttackCollider;
    public float singleAttackCooldown = 1f;
    public int singleAttackDamage = 5;
    
    [Header("AOE Attack")]
    public Collider aoeAttackCollider;
    public float aoeAttackCooldown = 1f;
    public int aoeAttackDamage = 3;
    
    [Header("Stun Attack")]
    public Collider stunCollider;
    public float stunCooldown = 1f;
    public float stunDuration = 1f;
    
    [Header("Player")]
    [SerializeField] private MovementAnimations movementAnimator;
    
    
    private PlayerStamina playerStamina;
    
    private Enemy currentSingleAttackTarget;
    private List<Enemy> currentAoeTargets;
    private List<Enemy> currentStunTargets;
    
    private bool inSingleAttack;
    private bool inAoeAttack;
    private bool inStun;

    
    private void Start()
    {
        currentAoeTargets = new List<Enemy>();
        currentStunTargets = new List<Enemy>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && singleAttackCollider.bounds.Intersects(other.bounds))
        {
            Debug.Log("Enemy in Single attack");
            Enemy enemy = other.GetComponent<Enemy>();
            if (currentSingleAttackTarget != enemy)
            {
                currentSingleAttackTarget = enemy;
            }
        }

        
        if (other.CompareTag("Enemy") && aoeAttackCollider.bounds.Intersects(other.bounds))
        {
            Debug.Log("Enemy in AOE!");
            Enemy enemy = other.GetComponent<Enemy>();
           
            if (!currentAoeTargets.Contains(enemy))
            {
                currentAoeTargets.Add(enemy);
            }
        }

        if (other.CompareTag("Enemy") && stunCollider.bounds.Intersects(other.bounds))
        {
            Debug.Log("Enemy in Stun");
            Enemy enemy = other.GetComponent<Enemy>();

            if (!currentStunTargets.Contains(enemy))
            {
                currentStunTargets.Add(enemy);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (currentSingleAttackTarget == enemy)
            {
                currentSingleAttackTarget = null;
            }

            if (currentAoeTargets.Contains(enemy))
            {
                currentAoeTargets.Remove(enemy);
            }

            if (currentStunTargets.Contains(enemy))
            {
                currentStunTargets.Remove(enemy);
            }
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inSingleAttack && playerStamina.CanSingleAttack())
        {
            playerStamina.DrainStamina(PlayerStamina.MovementType.SingleAttack);
            StartCoroutine(SingleAttackStart());
        }
        
        if (Input.GetMouseButtonDown(1) && !inAoeAttack && playerStamina.CanAoeAttack())
        {
            playerStamina.DrainStamina(PlayerStamina.MovementType.AoeAttack);
            StartCoroutine(AoeAttackStart());
        }

        if (Input.GetKeyDown(KeyCode.F) && !inStun && playerStamina.CanStun())
        {
            Debug.Log("Starting stun");
            StartCoroutine(StunStart());
        }
    }
    
    IEnumerator SingleAttackStart()
    {
        PlayerEventManager.TriggerOnAttack();
        inSingleAttack = true;
        movementAnimator.AnimateAttack();
        
        if (currentSingleAttackTarget != null)
        {
            Debug.Log("Im single attacking");
            currentSingleAttackTarget?.TakeDamage(singleAttackDamage, transform);
        }
        
        yield return new WaitForSeconds(singleAttackCooldown);
        
        inSingleAttack = false;
    }
    
    
    IEnumerator AoeAttackStart()
    {
        PlayerEventManager.TriggerOnAoe();
        inAoeAttack = true;
        movementAnimator.AnimateAoe();
        
        foreach (var target in currentAoeTargets)
        {
            Debug.Log("Giving damage");
            if (target != null)
            {
                target.TakeDamage(aoeAttackDamage,transform);
            }
        }
        
        yield return new WaitForSeconds(aoeAttackCooldown);
        
        inAoeAttack = false;
    }

    IEnumerator StunStart()
    {
        PlayerEventManager.TriggerOnStun();
        inStun = true;
        movementAnimator.AnimateAttack();

        foreach (var target in currentStunTargets)
        {
            Debug.Log("Stunning");
            if (target != null)
            {
                target.GetStun(stunDuration);
            }
        }
        
        yield return new WaitForSeconds(stunCooldown);
        
        inStun = false;
    }
    
}
