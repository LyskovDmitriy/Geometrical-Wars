using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{

	public GameObject gameOverMenuCanvas;
	public GameObject starsObject;
	public Image[] stars;
	public Button nextLevelButton;
	public Text gameOverText;
	public int[] starsForLives;
	public string nextLevel;


	public IEnumerator GameOver(float delay)
	{
		yield return new WaitForSeconds(delay);
		gameOverMenuCanvas.SetActive(true);
		nextLevelButton.interactable = false;
		FindObjectOfType<EnemyWavesManager>().enabled = false;
	}


	public IEnumerator LevelCompleted(float delay)
	{
		yield return new WaitForSeconds(delay);
		gameOverMenuCanvas.SetActive(true);
		gameOverText.text = "Level completed";
		starsObject.SetActive(true);
		int lives = FindObjectOfType<LivesManager>().CurrentLives;
		int starsAmount = stars.Length;

		for (int i = 0; i < stars.Length; i++)
		{
			if (lives < starsForLives[i])
			{
				stars[i].color = Color.black;
				starsAmount--;
			}
		}

		if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + " stars"))
		{
			if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " stars") >= starsAmount) //if player has more stars than he's earned this time
			{
				yield break;
			}
		}

		PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " stars", starsAmount);
	}


	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}


	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	public void NextLevel()
	{
		SceneManager.LoadScene(nextLevel);
	}


	public void QuitGame()
	{
		Application.Quit();
	}


	void Start()
	{
		if (nextLevel == "")
		{
			nextLevelButton.interactable = false;
		}
	}
}
