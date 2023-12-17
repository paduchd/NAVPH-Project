using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneIntroduction : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject introductionCanvas;
    [SerializeField] private GameObject garageIntroduction;
    [SerializeField] private GameObject sewersIntroduction;
    [SerializeField] private GameObject outskirtsIntroduction;
    
    private string scene;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        scene = SceneManager.GetActiveScene().name;
        
        GameOverScreenController.CurrentSceneName = scene;

        PauseMenu.canBePaused = false;
        
        if (scene == "Garage")
        {
            garageIntroduction.SetActive(true);
        }
        
        if (scene == "Sewers")
        {
            sewersIntroduction.SetActive(true);
        }
        
        if (scene == "Outskirts")
        {
            outskirtsIntroduction.SetActive(true);
        }
        
        PlayerEventManager.TriggerOnGamePause();
    }

    public void ContinueGame()
    {
        PauseMenu.canBePaused = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
        
        uiCanvas.gameObject.SetActive(true);
        introductionCanvas.gameObject.SetActive(false);
        
        PlayerEventManager.TriggerOnGameResume();
    }
}
