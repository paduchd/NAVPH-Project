using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EagleAttackTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownUI;
    private bool timerStarted = false;
    private float countdown;
    private float min;
    private float sec;

    private void OnEnable()
    {
        Eagle.OnEagleAttack += StartTimer;
    }

    private void OnDisable()
    {
        Eagle.OnEagleAttack -= StartTimer;
    }

    void Update()
    {
        if (timerStarted)
        {
            min = Mathf.FloorToInt((countdown - Time.deltaTime) / 60);
            sec = Mathf.FloorToInt((countdown - Time.deltaTime) % 60);
            countdownUI.text = $"The eagle is attacking! Hide in a bush before the timer ends!\n{min:00}:{sec:00}";
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                countdownUI.text = "";
            }
        }
    }

    private void StartTimer(float hidingTime)
    {
        countdown = hidingTime;
        timerStarted = true;
    }
}
