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

    [SerializeField] private float singleAttackCooldown = 1f;
    [SerializeField] private float aoeAttackCooldown = 1f;
    [SerializeField] private MovementAnimations movementAnimator;
    
    private PlayerStamina playerStamina;
    
    private bool inSingleAttack;
    private bool inAoeAttack;

    private void Start()
    {
        currentAOETargets = new List<Enemy>();
        playerStamina = GetComponent<PlayerStamina>();
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
            Debug.Log("Enemy in AOE!");
            Enemy enemy = other.GetComponent<Enemy>();
           
            if (!currentAOETargets.Contains(enemy))
            {
                currentAOETargets.Add(other.GetComponent<Enemy>());
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentSingleAttackTarget = null;
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
    }
    
    IEnumerator SingleAttackStart()
    {
        inSingleAttack = true;
        movementAnimator.AnimateAttack();
        
        if (currentSingleAttackTarget != null)
        {
            Debug.Log("Im single attacking");
            currentSingleAttackTarget?.TakeDamage(5,transform);
        }
        
        yield return new WaitForSeconds(singleAttackCooldown);
        
        inSingleAttack = false;
    }
    
    
    IEnumerator AoeAttackStart()
    {
        inAoeAttack = true;
        movementAnimator.AnimateAoe();
        
        foreach (var target in currentAOETargets)
        {
            Debug.Log("Giving damage");
            if (target != null)
            {
                target.TakeDamage(5,transform);
            }
        }
        
        yield return new WaitForSeconds(aoeAttackCooldown);
        
        inAoeAttack = false;
    }
    
}
