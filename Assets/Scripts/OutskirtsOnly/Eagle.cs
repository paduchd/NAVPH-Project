using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Random=UnityEngine.Random;
using UnityEngine;
using System;

public class Eagle : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float attackSpeed;
    [SerializeField] private float hidingTime;
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;
    [SerializeField] private int damageToPlayer;

    [Header("Components")] 
    public Transform player;

    public static event Action<float> OnEagleAttack;
    public static void TriggerOnEagleAttack(float timeToHide) => OnEagleAttack?.Invoke(timeToHide);

    private bool playerHidden = false;
    private bool canAttack = false;
    private bool attacking = false;
    private Vector3 playerPosition;
    private Vector3 scoutingPosition;
    private float countdown;

    public void HidePlayer()
    {
        playerHidden = true;
    }

    public void ShowPlayer()
    {
        playerHidden = false;
    }

    void Start()
    {
        scoutingPosition = transform.position;
    }

    void Update()
    {
        if (attacking)
        {
            AttackPlayer();
        }
        
        if (!attacking)
        {
            ReturnToScouting();
        }
    }

    private void AttackPlayer()
    {
        playerPosition = player.position;
        transform.LookAt(playerPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, attackSpeed * Time.deltaTime);
    }

    private void ReturnToScouting()
    {
        transform.LookAt(scoutingPosition);
        transform.position = Vector3.MoveTowards(transform.position, scoutingPosition, attackSpeed * Time.deltaTime);

        if (!canAttack)
        {
            canAttack = true;
            StartCoroutine(WaitForAttack());
        }
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(Random.Range(minAttackTime,maxAttackTime));
        TriggerOnEagleAttack(hidingTime);

        yield return new WaitForSeconds(hidingTime);
        attacking = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && playerHidden == false && canAttack)
        {
            col.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damageToPlayer,transform);
        }

        canAttack = false;
        attacking = false;
    }
}
