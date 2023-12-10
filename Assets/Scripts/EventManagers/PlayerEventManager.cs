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


}
