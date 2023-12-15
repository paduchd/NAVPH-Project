using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    private bool moving = false;
    private bool footstepsPlaying = false;
    
    [SerializeField] private AudioClip raccoonMoveConcrete;
    [SerializeField] private AudioClip raccoonMoveGrass;
    [SerializeField] private AudioClip raccoonSingleAttack;
    [SerializeField] private AudioClip raccoonAoeAttack;
    [SerializeField] private AudioClip raccoonStunAttack;
    [SerializeField] private AudioClip raccoonHit;
    [SerializeField] private AudioClip raccoonJump;
    [SerializeField] private AudioClip raccoonDash;

    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        PlayerEventManager.OnMovement += PlayMovementSound;
        PlayerEventManager.OnMovementStop += StopMovementSound;
        PlayerEventManager.OnDamaged += PlayDamageSound;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnMovement -= PlayMovementSound;
        PlayerEventManager.OnMovementStop -= StopMovementSound;
        PlayerEventManager.OnDamaged -= PlayDamageSound;
    }

    private void Update()
    {
        if (moving)
        {
            if (GameOverScreenController.CurrentSceneName == "Outskirts" && !footstepsPlaying)
            {
                footstepsPlaying = true;
                audioComponent.clip = raccoonMoveGrass;
                audioComponent.loop = true;
                audioComponent.volume = 0.02f;
                audioComponent.Play();
            }
        }

        if (!moving)
        {
            footstepsPlaying = false;
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
        audioComponent.volume = 0.02f;
        audioComponent.PlayOneShot(raccoonHit);
    }
}
