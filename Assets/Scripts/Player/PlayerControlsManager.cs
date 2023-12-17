using UnityEngine;

// Script that enables/disables racoons mechanics based on scene. Used when game is paused to not trigger inputs.
public class PlayerControlsManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerEventManager.OnGamePause += DisableControls;
        PlayerEventManager.OnGameResume += EnableControls;
    }

    private void OnDisable()
    {
        PlayerEventManager.OnGamePause -= DisableControls;
        PlayerEventManager.OnGameResume -= EnableControls;
    }

    private void DisableControls()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerJump>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<PlayerDash>().enabled = false;

    }

    private void EnableControls()
    {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerJump>().enabled = true;

        if (GameOverScreenController.CurrentSceneName == "Sewers")
        {
            GetComponent<PlayerAttack>().enabled = true;
        }
        
        if (GameOverScreenController.CurrentSceneName == "Outskirts")
        {
            GetComponent<PlayerAttack>().enabled = true;
            GetComponent<PlayerDash>().enabled = true;
        }
    }
}
