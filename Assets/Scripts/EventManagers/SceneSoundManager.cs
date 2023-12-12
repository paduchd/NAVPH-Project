using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    [SerializeField] private AudioClip bushSound;
    [SerializeField] private AudioClip boxFall;
    [SerializeField] private AudioClip ratAttack;
    [SerializeField] private AudioClip ratHit;
    [SerializeField] private AudioClip ratIdle;
    [SerializeField] private AudioClip eagleAttack;
    [SerializeField] private AudioClip eagleIdle;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip fallInWater;
    [SerializeField] private AudioClip foodCollect;
    
    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        BushController.OnBushEnter += PlayBushSound;
    }

    private void OnDisable()
    {
        BushController.OnBushEnter -= PlayBushSound;
    }

    private void PlayBushSound()
    {
        audioComponent.clip = bushSound;
        audioComponent.Play();
    }
    
}