using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEventManager : MonoBehaviour
{
    public static event Action OnDamaged;
    public static void TriggerOnDamaged() => OnDamaged?.Invoke();
    

    public static event Action OnHealthIncrease;
    public static void TriggerOnHealthIncrease() => OnHealthIncrease?.Invoke();
    

    public static event Action OnBoxFall;
    public static void TriggerOnBoxFall() => OnBoxFall?.Invoke();
    

    public static event Action OnPlayerTimerEscape;
    public static void TriggerOnPlayerTimerEscape() => OnPlayerTimerEscape?.Invoke();
    
    
    public static event Action<bool> OnFoodEaten;
    public static void TriggerOnFoodEaten(bool staminaIncreased) => OnFoodEaten?.Invoke(staminaIncreased);
    

    public static event Action OnStaminaUpdate;
    public static void TriggerOnStaminaUpdate() => OnStaminaUpdate?.Invoke();

    
    public static event Action OnDeath;
    public static void TriggerOnDeath() => OnDeath?.Invoke();


    public static event Action OnGamePause;
    public static void TriggerOnGamePause() => OnGamePause?.Invoke();


    public static event Action OnGameResume;
    public static void TriggerOnGameResume() => OnGameResume?.Invoke();
}
