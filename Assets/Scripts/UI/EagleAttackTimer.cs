using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Manages the part of UI which is showing eagle's arrival time countdown
public class EagleAttackTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownUI;
    
    private bool timerStarted = false;
    private float countdown;
    private float min;
    private float sec;

    // Subcribing to event
    private void OnEnable()
    {
        Eagle.OnEagleAttack += StartTimer;
    }

    // Unsubscribing to event
    private void OnDisable()
    {
        Eagle.OnEagleAttack -= StartTimer;
    }

    // Keeps the timer actual
    void Update()
    {
        if (timerStarted)
        {
            min = Mathf.FloorToInt((countdown - Time.deltaTime) / 60);
            sec = Mathf.FloorToInt((countdown - Time.deltaTime) % 60);
            countdownUI.text = $"The eagle is attacking! Hide in a bush until the eagle attacks you!\n{min:00}:{sec:00}";
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                countdownUI.text = "";
            }
        }
    }
    
    // Starts the timer after the occured event
    private void StartTimer(float hidingTime)
    {
        countdown = hidingTime;
        timerStarted = true;
    }
}
