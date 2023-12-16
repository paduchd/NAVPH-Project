using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    private bool moving = false;
    private bool footstepsPlaying = false;
    
    [SerializeField] private AudioClip raccoonMove;
    [SerializeField] private AudioClip[] raccoonScreams;
    [SerializeField] private AudioClip[] raccoonWhooshes;
    [SerializeField] private AudioClip raccoonStunAttack;
    [SerializeField] private AudioClip raccoonHit;

    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        PlayerEventManager.OnMovement += PlayMovementSound;
        PlayerEventManager.OnMovementStop += StopMovementSound;
        PlayerEventManager.OnDamaged += PlayDamageSound;
        PlayerEventManager.OnAttack += PlayScream;
        PlayerEventManager.OnAoe += PlayWhoosh;
        PlayerEventManager.OnDash += PlayWhoosh;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnMovement -= PlayMovementSound;
        PlayerEventManager.OnMovementStop -= StopMovementSound;
        PlayerEventManager.OnDamaged -= PlayDamageSound;
        PlayerEventManager.OnAttack -= PlayScream;
        PlayerEventManager.OnAoe -= PlayWhoosh;
        PlayerEventManager.OnDash -= PlayWhoosh;
    }

    private void Update()
    {
        if (moving && !audioComponent.isPlaying && PlayerMovement.isGrounded)
        {
            audioComponent.clip = raccoonMove;
            audioComponent.loop = true;
            audioComponent.volume = 0.05f;
            audioComponent.Play();
        }

        else 
        {
            audioComponent.loop = false;
        }
    }

    private void PlayMovementSound()
    {
        moving = true;
    }
    
    private void StopMovementSound()
    {
        moving = false;
    }
    
    private void PlayDamageSound()
    {
        audioComponent.volume = 0.05f;
        audioComponent.PlayOneShot(raccoonHit);
    }

    private void PlayScream()
    {
        audioComponent.volume = 0.05f;
        audioComponent.PlayOneShot(raccoonScreams[Random.Range(0, raccoonScreams.Length)]);
    }

    private void PlayWhoosh()
    {
        audioComponent.volume = 0.05f;
        audioComponent.PlayOneShot(raccoonWhooshes[Random.Range(0, raccoonWhooshes.Length)]);
    }
}
