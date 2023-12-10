using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        SceneSwitchEventManager.GarageSwitch += SwitchToGarage;
        SceneSwitchEventManager.SewersSwitch += SwitchToSewers;
        SceneSwitchEventManager.OutskirtsSwitch += SwitchToOutskirts;
        //SceneSwitchEventManager.JunkyardSwitch += SwitchToJunkyard;
    }
    
    private void OnDisable()
    {
        SceneSwitchEventManager.GarageSwitch -= SwitchToGarage;
        SceneSwitchEventManager.SewersSwitch -= SwitchToSewers;
        SceneSwitchEventManager.OutskirtsSwitch -= SwitchToOutskirts;
        //SceneSwitchEventManager.JunkyardSwitch += SwitchToJunkyard;
    }

    private void SwitchToGarage()
    {
        title.text = "Garage";
        objective.text = "Find a way out of the garage!";
        description.text = "You are a raccoon and have been kidnapped from your home - the junkyard. You wake up in a strange garage with only one way out. Use the environment and objects in the garage to reach the exit and to get closer to getting back home!";
            
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
            
        StartCoroutine(Load());
    }

    private void SwitchToSewers()
    {
        title.text = "Sewers";
        objective.text = "Find a way out of the sewers maze!";
        description.text = "After escaping the garage you find yourself deep inside the town's sewer system. Find the sewer exit and get out. Watch out! The area is guarded by rats, attack them with newly unlocked attacks or try to evade them.";
            
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
            
        StartCoroutine(Load());
    }
    
    private void SwitchToOutskirts()
    {
        title.text = "Outskirts";
        objective.text = "Find food to replenish your energy!";
        description.text = "After running through sewers the whole night you became exhausted. You don't have any energy left and therefore you can't run, jump or attack. Search the area for pieces of food which slowly replenish your stamina. Beware! An angry eagle is scouting the area and will try to attack you from time to time. Hide in bushes to counter its attacks.";
            
        uiCanvas.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
            
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(title.text);
        sceneLoader.allowSceneActivation = false;

        while (!sceneLoader.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(sceneLoader.progress / .9f);

            if (sceneLoader.progress >= .9f)
            {
                loadIndicator.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameOverScreenController.CurrentSceneName = title.text;
                    sceneLoader.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
