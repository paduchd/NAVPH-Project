using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

// Manages the after-death screen (GameOver)
public class GameOverScreenController : MonoBehaviour
{
    // Store current scene for restart
    public static string CurrentSceneName = "";

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Quit game button
    public void ExitGame()
    {
        Application.Quit();
    }
    
    // Restart button (Restarts current scene)
    public void Restart()
    {
        if (CurrentSceneName == "Garage")
        {
            SceneSwitchEventManager.TriggerGarageSwitch();
        }

        if (CurrentSceneName == "Sewers")
        {
            SceneSwitchEventManager.TriggerSewersSwitch();
        }

        if (CurrentSceneName == "Outskirts")
        {
            SceneSwitchEventManager.TriggerOutskirtsSwitch();
        }
    }
    
    // Main menu button
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
