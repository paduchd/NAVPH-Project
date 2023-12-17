using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    [SerializeField] private AudioClip[] bushSounds;
    [SerializeField] private AudioClip boxFall;
    [SerializeField] private AudioClip eagleAttack;
    [SerializeField] private AudioClip foodCollect;
    [SerializeField] private AudioClip valveTurn;
    [SerializeField] private AudioClip containerSearch;
    
    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        audioComponent.volume = 0.1f;
        BushController.OnBushEnter += PlayBushSound;
        PlayerEventManager.OnBoxFall += PlayBoxSound;
        PlayerEventManager.OnFoodEaten += PlayFoodSound;
        Eagle.EagleSound += PlayEagleSound;
        ValveInteraction.OnValveInteraction += PlayValveSound;
        SearchContainer.OnContainerSearch += PlaySearchSound;
    }

    private void OnDisable()
    {
        BushController.OnBushEnter -= PlayBushSound;
        PlayerEventManager.OnBoxFall -= PlayBoxSound;
        PlayerEventManager.OnFoodEaten -= PlayFoodSound;
        Eagle.EagleSound -= PlayEagleSound;
        ValveInteraction.OnValveInteraction -= PlayValveSound;
        SearchContainer.OnContainerSearch -= PlaySearchSound;
    }

    private void PlayBushSound()
    {
        audioComponent.PlayOneShot(bushSounds[Random.Range(0, bushSounds.Length)]);
    }

    private void PlayBoxSound()
    {
        audioComponent.PlayOneShot(boxFall);
    }

    private void PlayEagleSound()
    {
        audioComponent.PlayOneShot(eagleAttack);
    }

    private void PlayFoodSound(bool b)
    {
        audioComponent.PlayOneShot(foodCollect);
    }

    private void PlayValveSound()
    {
        audioComponent.PlayOneShot(valveTurn);
    }

    private void PlaySearchSound()
    {
        audioComponent.PlayOneShot(containerSearch);
    }
}