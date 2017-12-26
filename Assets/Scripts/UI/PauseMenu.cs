using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{

	public GameObject pauseMenuCanvas;


	private bool isPaused;


	public void QuitGame()
	{
		ActivatePause();
		Application.Quit();
	}


	public void RestartLevel()
	{
		ActivatePause();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	public void MainMenu()
	{
		ActivatePause();
		SceneManager.LoadScene("Main Menu");
	}


	public void ActivatePause()
	{
		if (isPaused)
		{
			pauseMenuCanvas.SetActive(false);
			Time.timeScale = 1.0f;
		}
		else
		{
			pauseMenuCanvas.SetActive(true);
			Time.timeScale = 0.0f;
		}
		isPaused = !isPaused;
	}


	void Start () 
	{
		isPaused = false;
	}
	

	void Update () 
	{
		if (Input.GetButtonDown("Cancel"))
		{
			ActivatePause();
		}
	}
}
