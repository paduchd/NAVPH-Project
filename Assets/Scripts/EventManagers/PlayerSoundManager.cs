using UnityEngine;
using Random=UnityEngine.Random;

// Class responsible for playing player sounds
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

    // Event listeners for players action
    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
        audioComponent.volume = 0.05f;
        PlayerEventManager.OnMovement += PlayMovementSound;
        PlayerEventManager.OnMovementStop += StopMovementSound;
        PlayerEventManager.OnDamaged += PlayDamageSound;
        PlayerEventManager.OnAttack += PlayScream;
        PlayerEventManager.OnAoe += PlayWhoosh;
        PlayerEventManager.OnDash += PlayWhoosh;
        PlayerEventManager.OnGamePause += StopSounds;
        PlayerEventManager.OnGameResume += ResumeSounds;
        PlayerEventManager.OnStun += PlayStun;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnMovement -= PlayMovementSound;
        PlayerEventManager.OnMovementStop -= StopMovementSound;
        PlayerEventManager.OnDamaged -= PlayDamageSound;
        PlayerEventManager.OnAttack -= PlayScream;
        PlayerEventManager.OnAoe -= PlayWhoosh;
        PlayerEventManager.OnDash -= PlayWhoosh;
        PlayerEventManager.OnGamePause -= StopSounds;
        PlayerEventManager.OnGameResume -= ResumeSounds;
        PlayerEventManager.OnStun -= PlayStun;
    }

    // Footsteps playing when walking
    private void Update()
    {
        if (moving && !audioComponent.isPlaying && PlayerMovement.isGrounded)
        {
            audioComponent.clip = raccoonMove;
            audioComponent.loop = true;
            audioComponent.Play();
        }

        else 
        {
            audioComponent.loop = false;
        }
    }

    // Functions for individual sounds based on events
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
        audioComponent.PlayOneShot(raccoonHit);
    }

    private void PlayScream()
    {
        audioComponent.PlayOneShot(raccoonScreams[Random.Range(0, raccoonScreams.Length)]);
    }

    private void PlayWhoosh()
    {
        audioComponent.PlayOneShot(raccoonWhooshes[Random.Range(0, raccoonWhooshes.Length)]);
    }

    private void StopSounds()
    {
        audioComponent.volume = 0f;
    }

    private void ResumeSounds()
    {
        audioComponent.volume = 0.05f;
    }
    
    private void PlayStun()
    {
        audioComponent.PlayOneShot(raccoonStunAttack);
    }
}
