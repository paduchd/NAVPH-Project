using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages pause menu of the game
public class PauseMenu : MonoBehaviour
{
    public static bool canBePaused = true;
    public AudioSource backgroundSound;
    
    public bool isPaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canBePaused)
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Sets back time and deactivates the menu UI
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        
        backgroundSound.Play();
        PlayerEventManager.TriggerOnGameResume();
    }

    // Stops the time and activates the menu UI
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
        
        backgroundSound.Pause();
        PlayerEventManager.TriggerOnGamePause();
    }

    public void ExitGame()
	{
		Application.Quit();
	}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
