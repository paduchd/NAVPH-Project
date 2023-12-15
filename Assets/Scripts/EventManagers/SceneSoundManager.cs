using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    [SerializeField] private AudioClip[] bushSounds;
    [SerializeField] private AudioClip boxFall;
    [SerializeField] private AudioClip eagleAttack;
    [SerializeField] private AudioClip fallInWater;
    [SerializeField] private AudioClip foodCollect;
    
    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        BushController.OnBushEnter += PlayBushSound;
        PlayerEventManager.OnBoxFall += PlayBoxSound;
        PlayerEventManager.OnFoodEaten += PlayFoodSound;
        Eagle.EagleSound += PlayEagleSound;
    }

    private void OnDisable()
    {
        BushController.OnBushEnter -= PlayBushSound;
        PlayerEventManager.OnBoxFall -= PlayBoxSound;
        PlayerEventManager.OnFoodEaten -= PlayFoodSound;
        Eagle.EagleSound -= PlayEagleSound;
    }

    private void PlayBushSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(bushSounds[Random.Range(0, bushSounds.Length)]);
    }

    private void PlayBoxSound()
    {
        audioComponent.volume = 0.01f;
        audioComponent.PlayOneShot(boxFall);
    }

    private void PlayEagleSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(eagleAttack);
    }

    private void PlayFoodSound(bool b)
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(foodCollect);
    }
    
}