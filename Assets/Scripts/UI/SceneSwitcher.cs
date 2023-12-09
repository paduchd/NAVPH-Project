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
        PlayerEventManager.OnGarageEscape += SwitchToSewers;
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
                    sceneLoader.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
