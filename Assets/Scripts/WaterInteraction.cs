using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private bool touchedWater;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int damageToPlayer = 2;
    [SerializeField] private float damageIntervalTime = 0.8f;
    private float time;

    private void Update()
    {
        if (playerHealth.currentHealth > 0 & touchedWater)
        {
            time += Time.deltaTime;

            if (time >= damageIntervalTime)
            {
                playerHealth.TakeDamage(damageToPlayer);
                time = 0;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !touchedWater)
        {
            playerHealth.TakeDamage(damageToPlayer);
            touchedWater = true;
        }
    }
}
