using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour 
{

	public string levelName;
	public GameObject[] stars;


	public void LoadLevel()
	{
		if (levelName != "")
		{
			if (levelName != "Main Menu")
			{
				PlayerPrefs.SetString("Last level", levelName);
			}
			SceneManager.LoadScene(levelName);
		}
	}


	void Start()
	{
		if ((levelName != "") && PlayerPrefs.HasKey(levelName + " stars"))
		{
			int starsAmount = PlayerPrefs.GetInt(levelName + " stars");

			for (int i = 0; i < starsAmount; i++)
			{
				stars[i].SetActive(true);
			}
		}
	}
}
