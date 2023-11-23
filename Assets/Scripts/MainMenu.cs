using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
	{
		SceneManager.LoadScene("Garage");
	}

	public void StartSewers()
	{
		SceneManager.LoadScene("Sewers");
	}

	public void StartOutskirts()
	{
		SceneManager.LoadScene("Outskirts");
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
