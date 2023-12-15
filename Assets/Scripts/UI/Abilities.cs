using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] private Image attackIcon;
    [SerializeField] private Image attackIconBg;
    [SerializeField] private Image aoeIcon;
    [SerializeField] private Image aoeIconBg;
    [SerializeField] private Image stunIcon;
    [SerializeField] private Image stunIconBg;
    [SerializeField] private Image dashIcon;
    [SerializeField] private Image dashIconBg;

    private bool attackOnCooldown = false;
    private float attackCooldown;
    
    private bool aoeOnCooldown = false;
    private float aoeCooldown;
    
    private bool stunOnCooldown = false;
    private float stunCooldown;
    
    private bool dashOnCooldown = false;
    private float dashCooldown;

    private void OnEnable()
    {
        attackCooldown = PlayerAttack.singleAttackCooldown;
        aoeCooldown = PlayerAttack.aoeAttackCooldown;
        dashCooldown = PlayerDash.dashCooldown;
        
        PlayerEventManager.OnAttack += UpdateAttackCooldown;
        PlayerEventManager.OnAoe += UpdateAoeCooldown;
        // PlayerEventManager.OnStun += UpdateStunCooldown;
        PlayerEventManager.OnDash += UpdateDashCooldown;
    }
    
    private void OnDisable()
    {
        PlayerEventManager.OnAttack -= UpdateAttackCooldown;
        PlayerEventManager.OnAoe -= UpdateAoeCooldown;
        // PlayerEventManager.OnStun -= UpdateStunCooldown;
        PlayerEventManager.OnDash -= UpdateDashCooldown;
    }

    void Start()
    {
        attackIcon.fillAmount = 1;
        aoeIcon.fillAmount = 1;
        stunIcon.fillAmount = 1;
        dashIcon.fillAmount = 1;
    }

    
    void Update()
    {
        if (attackOnCooldown)
        {
            attackIcon.fillAmount += 1 / attackCooldown * Time.deltaTime;

            if (attackIcon.fillAmount >= 1)
            {
                attackOnCooldown = false;
                attackIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
        
        if (aoeOnCooldown)
        {
            aoeIcon.fillAmount += 1 / aoeCooldown * Time.deltaTime;

            if (aoeIcon.fillAmount >= 1)
            {
                aoeOnCooldown = false;
                aoeIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
        
        if (dashOnCooldown)
        {
            dashIcon.fillAmount += 1 / dashCooldown * Time.deltaTime;

            if (dashIcon.fillAmount >= 1)
            {
                dashOnCooldown = false;
                dashIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
    }

    private void UpdateAttackCooldown()
    {
        attackIconBg.color = Color.grey;
        attackIcon.fillAmount = 0;
        attackOnCooldown = true;
    }
    
    private void UpdateAoeCooldown()
    {
        aoeIconBg.color = Color.grey;
        aoeIcon.fillAmount = 0;
        aoeOnCooldown = true;
    }
    
    private void UpdateDashCooldown()
    {
        dashIconBg.color = Color.grey;
        dashIcon.fillAmount = 0;
        dashOnCooldown = true;
    }
}
