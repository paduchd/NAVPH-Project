using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool canBePaused = true;
    
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

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        
        PlayerEventManager.TriggerOnGameResume();
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
        
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
