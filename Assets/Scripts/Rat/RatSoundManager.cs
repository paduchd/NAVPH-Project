using UnityEngine;
using Random=UnityEngine.Random;

// Sound manager for all rat sounds
public class RatSoundManager : MonoBehaviour
{
    private AudioSource audioComponent;

    [SerializeField] private AudioClip[] ratSqueaks;
    [SerializeField] private AudioClip ratDeath;
    [SerializeField] private AudioClip ratAttack;

    [SerializeField] private float volume = 0.005f;
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
        audioComponent.volume = volume;
        audioComponent.PlayOneShot(ratAttack);
    }

    private void PlayDeathSound()
    {
        audioComponent.volume = volume;
        audioComponent.PlayOneShot(ratDeath);
    }
    
    private void PlayHitSound()
    {
        audioComponent.volume = volume;
        audioComponent.PlayOneShot(ratSqueaks[Random.Range(0, ratSqueaks.Length)]);
    }
}
