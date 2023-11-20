using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EagleController : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float countdownTime;
    
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
        min = Mathf.FloorToInt((countdownTime - Time.deltaTime) / 60);
        sec = Mathf.FloorToInt((countdownTime - Time.deltaTime) % 60);
        timer.text = string.Format("The eagle is attacking! Hide in a bush before the timer ends!\n{0:00}:{1:00}", min, sec);
        countdownTime -= Time.deltaTime;

        if (countdownTime <= 0)
        {
            timer.text = "";
            playerPosition = player.transform.position;
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        }

        if (canAttack == false)
        {
            transform.LookAt(startingPosition);
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);
            if (transform.position == startingPosition)
            {
                canAttack = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("playerHidden: " + playerHidden + "canAttack: " + canAttack);
        if(col.gameObject.tag == "Player" && playerHidden == false && canAttack == true)
        {
            col.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(2,transform);
        }
        canAttack = false;
        countdownTime = 10;
    }

    private void resetTimer()
    {
        countdownTime = 10;
    }
}
