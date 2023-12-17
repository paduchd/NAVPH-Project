using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Class which manages the abilities UI part
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

    // On enabling needs to get the separate cooldowns and add event listener
    private void OnEnable()
    {
        attackCooldown = PlayerAttack.singleAttackCooldown;
        aoeCooldown = PlayerAttack.aoeAttackCooldown;
        dashCooldown = PlayerDash.dashCooldown;
        stunCooldown = PlayerAttack.stunCooldown;
        
        PlayerEventManager.OnAttack += UpdateAttackCooldown;
        PlayerEventManager.OnAoe += UpdateAoeCooldown;
        PlayerEventManager.OnStun += UpdateStunCooldown;
        PlayerEventManager.OnDash += UpdateDashCooldown;
    }
    
    // Unsubscribing from event
    private void OnDisable()
    {
        PlayerEventManager.OnAttack -= UpdateAttackCooldown;
        PlayerEventManager.OnAoe -= UpdateAoeCooldown;
        PlayerEventManager.OnStun -= UpdateStunCooldown;
        PlayerEventManager.OnDash -= UpdateDashCooldown;
    }

    // Initialization of ability UI
    void Start()
    {
        attackIcon.fillAmount = 1;
        aoeIcon.fillAmount = 1;
        stunIcon.fillAmount = 1;
        dashIcon.fillAmount = 1;
    }

    // Manages showcasing the current cooldowns on abilities
    void Update()
    {
        // single attack cooldown
        if (attackOnCooldown)
        {
            attackIcon.fillAmount += 1 / attackCooldown * Time.deltaTime;

            if (attackIcon.fillAmount >= 1)
            {
                attackOnCooldown = false;
                attackIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
        
        // aoe attack cooldown
        if (aoeOnCooldown)
        {
            aoeIcon.fillAmount += 1 / aoeCooldown * Time.deltaTime;

            if (aoeIcon.fillAmount >= 1)
            {
                aoeOnCooldown = false;
                aoeIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
        
        // dash cooldown
        if (dashOnCooldown)
        {
            dashIcon.fillAmount += 1 / dashCooldown * Time.deltaTime;

            if (dashIcon.fillAmount >= 1)
            {
                dashOnCooldown = false;
                dashIconBg.color = new Color(1.0f, 0.66f, 0.0f);
            }
        }
        
        // stun cooldown
        if (stunOnCooldown)
        {
            stunIcon.fillAmount += 1 / stunCooldown * Time.deltaTime;

            if (stunIcon.fillAmount >= 1)
            {
                stunOnCooldown = false;
                stunIconBg.color = new Color(1.0f, 0.66f, 0.0f);
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
    
    private void UpdateStunCooldown()
    {
        stunIconBg.color = Color.grey;
        stunIcon.fillAmount = 0;
        stunOnCooldown = true;
    }
}
