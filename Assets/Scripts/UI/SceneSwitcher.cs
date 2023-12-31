using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages loading screens between the scenes
public class SceneSwitcher : MonoBehaviour
{
    [Header("Components")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI objective;
    public TextMeshProUGUI description;
    public TextMeshProUGUI loadIndicator;
    public GameObject uiCanvas;
    public GameObject loadingCanvas;
    public Image loadingBar;
    public RawImage background;
    public AudioSource backgroundSound;
    private AudioSource narrator;
    private String sceneName;
    
    [Header("Backgrounds")]
    public Texture2D garageImage; 
    public Texture2D sewersImage; 
    public Texture2D outskirtsImage;
    public Texture2D finalImage;
    
    
    [Header("Sounds")]
    public AudioClip garageNarrator;
    public AudioClip sewersNarrator;
    public AudioClip outskirtsNarrator;
    public AudioClip finalNarrator;

    
    // Event listeners for scene switch events
    private void OnEnable()
    {
        narrator = GetComponent<AudioSource>();
        narrator.volume = 0.1f;
        SceneSwitchEventManager.GarageSwitch += SwitchToGarage;
        SceneSwitchEventManager.SewersSwitch += SwitchToSewers;
        SceneSwitchEventManager.OutskirtsSwitch += SwitchToOutskirts;
        SceneSwitchEventManager.JunkyardSwitch += SwitchToJunkyard;
    }
    
    // Unsubscribing to events
    private void OnDisable()
    {
        SceneSwitchEventManager.GarageSwitch -= SwitchToGarage;
        SceneSwitchEventManager.SewersSwitch -= SwitchToSewers;
        SceneSwitchEventManager.OutskirtsSwitch -= SwitchToOutskirts;
        SceneSwitchEventManager.JunkyardSwitch -= SwitchToJunkyard;
    }

    
    // Garage loading screen
    private void SwitchToGarage()
    {
        backgroundSound.Stop();
        PlayerEventManager.TriggerOnGamePause();
        sceneName = "Garage";
        title.text = sceneName;
        objective.text = "Find a way out of the garage!";
        description.text = "Poor raccoon! You have been kidnapped from your home - the junkyard. Now you wake up in a strange garage with only one way out. Use the environment and objects in the garage to reach the exit and to get closer to getting back home!";
        background.texture = garageImage;
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        narrator.clip = garageNarrator;
        narrator.Play();
            
        StartCoroutine(Load());
    }
    
    // Sewers loading screen
    private void SwitchToSewers()
    {
        backgroundSound.Stop();
        PlayerEventManager.TriggerOnGamePause();
        sceneName = "Sewers";
        title.text = sceneName;
        objective.text = "Find a way out of the sewers maze!";
        description.text = "After escaping the garage you find yourself deep inside the town's sewer system. Find the sewer exit and get out. Watch out! The area is guarded by rats, attack them with newly unlocked attacks or try to evade them. The choice is yours.";
        background.texture = sewersImage;
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        narrator.PlayOneShot(sewersNarrator);
            
        StartCoroutine(Load());
    }
    
    // Outskirts loading screen
    private void SwitchToOutskirts()
    {
        backgroundSound.Stop();
        PlayerEventManager.TriggerOnGamePause();
        sceneName = "Outskirts";
        title.text = sceneName;
        objective.text = "Find food to replenish your energy!";
        description.text = "After running through sewers the whole night you became exhausted. You don't have any energy left and therefore you can't run, jump or attack. Search the area for pieces of food which slowly replenish your stamina. Beware! An angry eagle is scouting the outskirts and will try to attack you from time to time. Maybe bushes can help you with hiding.";
        background.texture = outskirtsImage;
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        narrator.clip = outskirtsNarrator;
        narrator.Play();
            
        StartCoroutine(Load());
    }

    // Final loading screen - had to be junkyard but the game finishes after outskirts
    private void SwitchToJunkyard()
    {
        backgroundSound.Stop();
        PlayerEventManager.TriggerOnGamePause();
        sceneName = "MainMenu";
        title.text = "The End";
        objective.text = "";
        description.text = "Congratulations! You have successfully escaped your kidnappers, found you way around the sewers maze, evaded the eagle's attacks and safely managed to get back home. Now rest, little adventurer.";
        background.texture = finalImage;
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        narrator.clip = finalNarrator;
        narrator.Play();
            
        StartCoroutine(Load());
    }

    // Coroutine which loads the next scene in the background and shows progress on the loading screen
    IEnumerator Load()
    {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneName);
        sceneLoader.allowSceneActivation = false;

        while (!sceneLoader.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(sceneLoader.progress / .9f);

            if (sceneLoader.progress >= .9f)
            {
                loadIndicator.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    sceneLoader.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
