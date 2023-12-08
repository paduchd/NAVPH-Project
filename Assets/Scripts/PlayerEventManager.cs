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
    
}
