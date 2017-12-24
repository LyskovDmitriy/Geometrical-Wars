using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

	public Button resumeButton;


	public void ResumeGame()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("Last level"));
	}


	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}


	public void QuitGame()
	{
		Application.Quit();
	}


	public void Start()
	{
		if (!PlayerPrefs.HasKey("Last level"))
		{
			resumeButton.interactable = false;
		}
	}
}
