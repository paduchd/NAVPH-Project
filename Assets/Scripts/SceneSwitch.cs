using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneSwitch : MonoBehaviour
{
    [Header("Scene information")]
    [SerializeField] public string SceneName;
    [SerializeField] public string SceneObjective;
    [SerializeField] public string SceneDescription;
    
    [Header("Components")]
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI LoadIndicator;
    public GameObject UICanvas;
    public GameObject LoadingCanvas;
    public Image LoadingBar;
    
    void Start()
    {
        
    }

    
    void Update()
    {
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Title.text = SceneName;
            Objective.text = SceneObjective;
            Description.text = SceneDescription;
            
            UICanvas.gameObject.SetActive(false);
            LoadingCanvas.gameObject.SetActive(true);
            
            StartCoroutine(Load());
        }
    }

    IEnumerator Load()
    {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(SceneName);
        sceneLoader.allowSceneActivation = false;

        while (!sceneLoader.isDone)
        {
            LoadingBar.fillAmount = Mathf.Clamp01(sceneLoader.progress / .9f);

            if (sceneLoader.progress >= .9f)
            {
                LoadIndicator.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    sceneLoader.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
