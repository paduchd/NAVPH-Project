using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;
    [SerializeField] private AudioClip raccoonSingleAttack;
    [SerializeField] private AudioClip raccoonAoeAttack;
    [SerializeField] private AudioClip raccoonStunAttack;
    [SerializeField] private AudioClip raccoonHit;
    [SerializeField] private AudioClip raccoonJump;
    [SerializeField] private AudioClip raccoonDash;

    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        
    }

    private void PlayBushSound()
    {

    }
}
