using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void StartGame()
	{
		SceneSwitchEventManager.TriggerGarageSwitch();
	}

	public void StartSewers()
	{
		SceneSwitchEventManager.TriggerSewersSwitch();
	}

	public void StartOutskirts()
	{
		SceneSwitchEventManager.TriggerOutskirtsSwitch();
	}

	public void StartJunkyard()
	{
		SceneManager.LoadScene("Junkyard");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
