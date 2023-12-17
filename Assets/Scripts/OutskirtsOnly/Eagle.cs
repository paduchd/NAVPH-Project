using System.Collections;
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
    public static event Action EagleSound;
    public static void TriggerEagleSound() => EagleSound?.Invoke();
    

    private bool playerHidden = false;
    private bool canAttack = false;
    private bool attacking = false;
    private Vector3 playerPosition;
    private Vector3 scoutingPosition;
    private float countdown;

    // Functions for showing and hiding player from the eagle (eagle cant attack hidden player)
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
        Physics.IgnoreLayerCollision(9, 11);
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

    // Moves eagle towards player
    private void AttackPlayer()
    {
        playerPosition = player.position;
        transform.LookAt(playerPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, attackSpeed * Time.deltaTime);
    }

    // Returns to starting position after attack
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

    // Random time between attacks
    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(Random.Range(minAttackTime,maxAttackTime));
        TriggerOnEagleAttack(hidingTime);

        yield return new WaitForSeconds(hidingTime);
        TriggerEagleSound();
        attacking = true;
    }

    // Attacks player on collision if player is not hidden
    private void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("PlayerAttackBox")) && playerHidden == false && canAttack)
        {
            col.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damageToPlayer,transform);
        }

        canAttack = false;
        attacking = false;
    }
}
