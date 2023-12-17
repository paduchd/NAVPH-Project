using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneSwitchEventManager : MonoBehaviour
{
    // Events used for scene switching
    public static event Action GarageSwitch;
    public static void TriggerGarageSwitch() => GarageSwitch?.Invoke();
    
    public static event Action SewersSwitch;
    public static void TriggerSewersSwitch() => SewersSwitch?.Invoke();
    
    public static event Action OutskirtsSwitch;
    public static void TriggerOutskirtsSwitch() => OutskirtsSwitch?.Invoke();

    public static event Action JunkyardSwitch;
    public static void TriggerJunkyardSwitch() => JunkyardSwitch?.Invoke();
}
