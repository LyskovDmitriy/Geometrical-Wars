using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCounter : MonoBehaviour 
{

	public int TotalStars
	{ get; private set;}


	public Text starsNumber;
	public string[] towerNames;
	public int numberOfLevels;


	public void Calculate()
	{
		int starsAmount = 0;

		for (int i = 1; i <= numberOfLevels; i++)
		{
			string saveName = "Level " + i + " stars";

			if (PlayerPrefs.HasKey(saveName))
			{
				starsAmount += PlayerPrefs.GetInt(saveName);
			}
		}

		foreach (string name in towerNames)
		{
			IceTowerUpgrade info = SaveLoad.Load(name);
			starsAmount -= info.starsPrice;
		}

		starsNumber.text = starsAmount.ToString();
		TotalStars = starsAmount;
	}
		

	void Start ()
	{
		Calculate();
	}

}
