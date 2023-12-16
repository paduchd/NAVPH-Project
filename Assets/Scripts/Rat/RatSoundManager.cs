using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class RatSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;

    [SerializeField] private AudioClip[] ratSqueaks;
    [SerializeField] private AudioClip ratDeath;
    [SerializeField] private AudioClip ratAttack;

    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();

        Enemy.OnRatAttack += PlayAttackSound;
        Enemy.OnRatDeath += PlayDeathSound;
        Enemy.OnRatHit += PlayHitSound;
    }

    private void OnDisable()
    {
        Enemy.OnRatAttack -= PlayAttackSound;
        Enemy.OnRatDeath -= PlayDeathSound;
        Enemy.OnRatHit -= PlayHitSound;
    }

    private void PlayAttackSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(ratAttack);
    }

    private void PlayDeathSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(ratDeath);
    }
    
    private void PlayHitSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(ratSqueaks[Random.Range(0, ratSqueaks.Length)]);
    }
}
