using UnityEngine;
using System;

// Class defining all events the player can trigger
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


    public static event Action OnMovement;
    public static void TriggerOnMovement() => OnMovement?.Invoke();


    public static event Action OnMovementStop;
    public static void TriggerOnMovementStop() => OnMovementStop?.Invoke();


    public static event Action OnAttack;
    public static void TriggerOnAttack() => OnAttack?.Invoke();


    public static event Action OnAoe;
    public static void TriggerOnAoe() => OnAoe?.Invoke();


    public static event Action OnStun;
    public static void TriggerOnStun() => OnStun?.Invoke();


    public static event Action OnDash;
    public static void TriggerOnDash() => OnDash?.Invoke();

    public static event Action OnInteractionEnter;
    public static void TriggerOnInteractionEnter() => OnInteractionEnter?.Invoke();

    public static event Action OnInteractionExit;
    public static void TriggerOnInteractionExit() => OnInteractionExit?.Invoke();
}
