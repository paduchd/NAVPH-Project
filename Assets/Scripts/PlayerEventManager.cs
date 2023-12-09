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

    public static event Action OnGarageEscape;
    public static void TriggerOnGarageEscape() => OnGarageEscape?.Invoke();
    
    public static event Action OnSewersEscape;
    public static void TriggerOnSewersEscape() => OnSewersEscape?.Invoke();
    
    public static event Action OnOutskirtsEscape;
    public static void TriggerOnOutskirtsEscape() => OnOutskirtsEscape?.Invoke();
    
    public static event Action OnBoxFall;
    public static void TriggerOnBoxFall() => OnBoxFall?.Invoke();

    public static event Action OnPlayerTimerEscape;
    public static void TriggerOnPlayerTimerEscape() => OnPlayerTimerEscape?.Invoke();
    
    public static event Action<bool> OnFoodEaten;
    public static void TriggerOnFoodEaten(bool staminaIncreased) => OnFoodEaten?.Invoke(staminaIncreased);


}
