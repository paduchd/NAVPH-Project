using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EagleController : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float countdownTime;
    public int playerDamage;
    
    [Header("Components")]
    public GameObject player;
    public TextMeshProUGUI timer;
    
    private bool playerHidden = false;
    private bool canAttack = true;
    private Vector3 playerPosition;
    private Vector3 startingPosition;
    private float min;
    private float sec;
    
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
        startingPosition = transform.position;
    }
    
    void Update()
    {
        if (canAttack)
        {
            UpdateTimer();
        }

        if (canAttack && countdownTime <= 0)
        {
            AttackPlayer();
        }

        if (!canAttack)
        {
            ReturnToScouting();
        }
    }

    private void UpdateTimer()
    {
        min = Mathf.FloorToInt((countdownTime - Time.deltaTime) / 60);
        sec = Mathf.FloorToInt((countdownTime - Time.deltaTime) % 60);
        timer.text = string.Format("The eagle preparing for an attack! Hide in a bush before the timer ends!\n{0:00}:{1:00}", min, sec);
        countdownTime -= Time.deltaTime;
    }

    private void ResetTimer()
    {
        countdownTime = 20;
    }

    private void AttackPlayer()
    {
        timer.text = "The eagle is attacking!";

        playerPosition = player.transform.position;
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
    }

    private void ReturnToScouting()
    {
        transform.LookAt(startingPosition);
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);

        if (transform.position == startingPosition)
            {
                canAttack = true;
                ResetTimer();
            }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && playerHidden == false && canAttack == true)
        {
            col.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(playerDamage,transform);
        }

        timer.text = "";
        canAttack = false;
    }
}
