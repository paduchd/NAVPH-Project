using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    // All sounds which can be triggered in scenes
    private AudioSource audioComponent;
    [SerializeField] private AudioClip[] bushSounds;
    [SerializeField] private AudioClip boxFall;
    [SerializeField] private AudioClip eagleAttack;
    [SerializeField] private AudioClip foodCollect;
    [SerializeField] private AudioClip valveTurn;
    [SerializeField] private AudioClip containerSearch;
    
    // Event listeners
    private void OnEnable()
    {
        audioComponent = GetComponent<AudioSource>();
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

    // Functions fo individual sounds based on events
    private void PlayBushSound()
    {
        audioComponent.PlayOneShot(bushSounds[Random.Range(0, bushSounds.Length)]);
    }

    private void PlayBoxSound()
    {
        audioComponent.volume = 0.02f;
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

    private void PlayValveSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(valveTurn);
    }

    private void PlaySearchSound()
    {
        audioComponent.volume = 0.1f;
        audioComponent.PlayOneShot(containerSearch);
    }
}