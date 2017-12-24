using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour 
{

	public int CurrentLives
	{
		get; private set;
	}


	public static bool isAlive;


	public int maxLives;
	public int bossDamageToLives;


	private Text livesText;


	public void TakeLife()
	{
		CurrentLives--;
		livesText.text = "Lives: " + CurrentLives;
		if (CurrentLives <= 0)
		{
			CurrentLives = 0;
			if (isAlive)
			{
				isAlive = false;
				StartCoroutine(FindObjectOfType<GameOverMenu>().GameOver(2.0f));
			}
		}
	}
		

	public void BossAttack()
	{
		for (int i = 0; i < bossDamageToLives; i++)
		{
			TakeLife();
		}
	}

	
	void Awake () 
	{
		livesText = GetComponent<Text>();
	}


	void Start()
	{
		CurrentLives = maxLives;
		livesText.text = "Lives: " + CurrentLives;
		isAlive = true;
	}
}
