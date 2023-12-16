using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutskirtsRat : MonoBehaviour, IDamageable
{
    public Transform player;
    public GameObject rewardFood;
    private bool isAlive = true;

    private void OnEnable()
    {
        Enemy.OnRatDeath += SpawnReward;
    }

    private void Update()
    {
        if (isAlive)
            transform.LookAt(player);
    }

    private void SpawnReward()
    {
        isAlive = false;
        rewardFood.SetActive(true);
    }
}
